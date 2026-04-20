param(
    [string]$DocsDir = $PSScriptRoot
)

$ErrorActionPreference = 'Stop'

function Build-Guide {
    param(
        [string]$SourcePath,
        [string]$OutputPath,
        [string]$FooterText
    )

    if (-not (Test-Path $SourcePath)) { throw "Source not found: $SourcePath" }
    if (Test-Path $OutputPath) { Remove-Item $OutputPath -Force }

    Write-Host ""
    Write-Host "============================================================"
    Write-Host "Building $([System.IO.Path]::GetFileName($OutputPath))"
    Write-Host "  source: $SourcePath"
    Write-Host "  output: $OutputPath"
    Write-Host "============================================================"

    $sourceDir = Split-Path -Parent $SourcePath
    $lines = Get-Content -LiteralPath $SourcePath -Encoding UTF8

    $word = New-Object -ComObject Word.Application
    $word.Visible = $false
    $word.DisplayAlerts = 0

    try {
        $doc = $word.Documents.Add()

        $ps = $doc.PageSetup
        $ps.TopMargin    = 72
        $ps.BottomMargin = 72
        $ps.LeftMargin   = 72
        $ps.RightMargin  = 72

        $normal = $doc.Styles.Item('Normal')
        $normal.Font.Name = 'Calibri'
        $normal.Font.Size = 11

        $sel = $word.Selection

        function Write-Runs {
            param($sel, $text)
            $runs = [regex]::Split($text, '(\*\*[^*]+\*\*)')
            foreach ($run in $runs) {
                if ([string]::IsNullOrEmpty($run)) { continue }
                if ($run.StartsWith('**') -and $run.EndsWith('**') -and $run.Length -gt 4) {
                    $sel.Font.Bold = $true
                    $sel.TypeText($run.Substring(2, $run.Length - 4))
                    $sel.Font.Bold = $false
                } else {
                    $sel.TypeText($run)
                }
            }
        }

        function Flush-Paragraph { param($sel, $text, $style)
            if ([string]::IsNullOrWhiteSpace($text)) { return }
            $sel.Style = $doc.Styles.Item($style)
            Write-Runs $sel $text
            $sel.TypeParagraph()
        }

        function Flush-Bullet { param($sel, $text)
            $sel.Style = $doc.Styles.Item('List Bullet')
            Write-Runs $sel $text
            $sel.TypeParagraph()
        }

        function Flush-Numbered { param($sel, $text)
            $sel.Style = $doc.Styles.Item('List Number')
            Write-Runs $sel $text
            $sel.TypeParagraph()
        }

        function Flush-Note { param($sel, $text)
            $sel.Style = $doc.Styles.Item('Quote')
            $sel.Font.Italic = $true
            Write-Runs $sel $text
            $sel.Font.Italic = $false
            $sel.TypeParagraph()
        }

        function Insert-Image { param($sel, $caption, $path, $sourceDir)
            $resolved = $path
            if (-not [System.IO.Path]::IsPathRooted($resolved)) {
                $resolved = Join-Path $sourceDir $path
            }
            $resolved = [System.IO.Path]::GetFullPath($resolved)

            $sel.Style = $doc.Styles.Item('Normal')
            $sel.ParagraphFormat.Alignment = 1  # wdAlignParagraphCenter
            if (Test-Path $resolved) {
                $shape = $sel.InlineShapes.AddPicture($resolved, $false, $true)
                # Scale to at most 5.5 inches wide
                $maxW = 5.5 * 72
                if ($shape.Width -gt $maxW) {
                    $ratio = $maxW / $shape.Width
                    $shape.Width = $maxW
                    $shape.Height = $shape.Height * $ratio
                }
                $sel.TypeParagraph()
                # Caption
                $sel.Style = $doc.Styles.Item('Caption')
                $sel.Font.Italic = $true
                $sel.TypeText("Figure: $caption")
                $sel.Font.Italic = $false
                $sel.TypeParagraph()
            } else {
                $sel.Style = $doc.Styles.Item('Quote')
                $sel.Font.Italic = $true
                $sel.TypeText("[image not found: $resolved]")
                $sel.Font.Italic = $false
                $sel.TypeParagraph()
            }
            $sel.ParagraphFormat.Alignment = 0  # wdAlignParagraphLeft
        }

        $isFirstH1 = $true
        $coverSubtitleHandled = $false
        $buffer = ''

        for ($i = 0; $i -lt $lines.Count; $i++) {
            $line = $lines[$i]

            if ($line -match '^---\s*$') {
                if ($buffer) { Flush-Paragraph $sel $buffer 'Normal'; $buffer = '' }
                $sel.InsertBreak(7)
                continue
            }
            if ($line -match '^!\[([^\]]*)\]\(([^)]+)\)\s*$') {
                if ($buffer) { Flush-Paragraph $sel $buffer 'Normal'; $buffer = '' }
                Insert-Image $sel $Matches[1] $Matches[2] $sourceDir
                continue
            }
            if ($line -match '^#\s+(.*)$') {
                if ($buffer) { Flush-Paragraph $sel $buffer 'Normal'; $buffer = '' }
                $text = $Matches[1]
                if ($isFirstH1) {
                    $sel.Style = $doc.Styles.Item('Title')
                    $sel.TypeText($text)
                    $sel.TypeParagraph()
                    $isFirstH1 = $false
                } else {
                    Flush-Paragraph $sel $text 'Heading 1'
                }
                continue
            }
            if ($line -match '^##\s+(.*)$') {
                if ($buffer) { Flush-Paragraph $sel $buffer 'Normal'; $buffer = '' }
                Flush-Paragraph $sel $Matches[1] 'Heading 2'
                continue
            }
            if ($line -match '^###\s+(.*)$') {
                if ($buffer) { Flush-Paragraph $sel $buffer 'Normal'; $buffer = '' }
                Flush-Paragraph $sel $Matches[1] 'Heading 3'
                continue
            }
            if ($line -match '^-\s+(.*)$') {
                if ($buffer) { Flush-Paragraph $sel $buffer 'Normal'; $buffer = '' }
                Flush-Bullet $sel $Matches[1]
                continue
            }
            if ($line -match '^(\d+)\.\s+(.*)$') {
                if ($buffer) { Flush-Paragraph $sel $buffer 'Normal'; $buffer = '' }
                Flush-Numbered $sel $Matches[2]
                continue
            }
            if ($line -match '^>\s*(.*)$') {
                if ($buffer) { Flush-Paragraph $sel $buffer 'Normal'; $buffer = '' }
                Flush-Note $sel $Matches[1]
                continue
            }
            if ($line -match '^\|') {
                # Table row — collect consecutive rows and emit as a Word table
                if ($buffer) { Flush-Paragraph $sel $buffer 'Normal'; $buffer = '' }
                $rows = @()
                while ($i -lt $lines.Count -and $lines[$i] -match '^\|') {
                    $rows += $lines[$i]
                    $i++
                }
                $i--  # back up one; outer for will advance
                # Drop separator row (| --- | --- |)
                $rows = $rows | Where-Object { $_ -notmatch '^\|\s*-+\s*(\|\s*-+\s*)+\|\s*$' }
                if ($rows.Count -gt 0) {
                    $cells = @()
                    foreach ($r in $rows) {
                        $parts = $r.Trim().Trim('|').Split('|') | ForEach-Object { $_.Trim() }
                        $cells += ,$parts
                    }
                    $colCount = ($cells | ForEach-Object { $_.Count } | Measure-Object -Maximum).Maximum
                    $rowCount = $cells.Count
                    $rangeStart = $sel.Range
                    $table = $doc.Tables.Add($rangeStart, $rowCount, $colCount)
                    $table.Borders.Enable = $true
                    for ($r = 0; $r -lt $rowCount; $r++) {
                        for ($c = 0; $c -lt [Math]::Min($colCount, $cells[$r].Count); $c++) {
                            $cell = $table.Cell($r + 1, $c + 1)
                            $cell.Range.Text = $cells[$r][$c]
                            if ($r -eq 0) {
                                $cell.Range.Font.Bold = $true
                            }
                        }
                    }
                    # Move selection to after the table
                    $sel.EndKey(6) | Out-Null  # wdStory
                    $sel.TypeParagraph()
                }
                continue
            }
            if ($line -match '^\*([^*].*[^*])\*\s*$') {
                if ($buffer) { Flush-Paragraph $sel $buffer 'Normal'; $buffer = '' }
                $sel.Font.Italic = $true
                $sel.TypeText($Matches[1])
                $sel.Font.Italic = $false
                $sel.TypeParagraph()
                continue
            }
            if ([string]::IsNullOrWhiteSpace($line)) {
                if ($buffer) { Flush-Paragraph $sel $buffer 'Normal'; $buffer = '' }
                continue
            }

            if (-not $coverSubtitleHandled -and -not $isFirstH1 -and $buffer -eq '') {
                $sel.Style = $doc.Styles.Item('Subtitle')
                $sel.TypeText($line)
                $sel.TypeParagraph()
                $coverSubtitleHandled = $true
                continue
            }

            if ($buffer) { $buffer = "$buffer $line" } else { $buffer = $line }
        }
        if ($buffer) { Flush-Paragraph $sel $buffer 'Normal' }

        # TOC at the top
        Write-Host "  adding table of contents..."
        $tocStart = $doc.Range(0, 0)
        $tocStart.InsertParagraphBefore()
        $tocStart = $doc.Range(0, 0)
        $tocStart.Style = $doc.Styles.Item('Heading 1')
        $tocStart.Text = "Table of Contents`r`n"
        $tocRange = $doc.Range($tocStart.End, $tocStart.End)
        $doc.TablesOfContents.Add($tocRange, $true, 1, 3, $false) | Out-Null
        $afterToc = $doc.TablesOfContents.Item(1).Range
        $breakRange = $doc.Range($afterToc.End, $afterToc.End)
        $breakRange.InsertBreak(7)

        # Footer
        Write-Host "  adding footer..."
        foreach ($section in $doc.Sections) {
            $footer = $section.Footers.Item(1)
            $footer.Range.Text = $FooterText
            $footer.Range.ParagraphFormat.Alignment = 1
            $footer.PageNumbers.Add(3, $false) | Out-Null
        }

        foreach ($toc in $doc.TablesOfContents) { $toc.Update() }

        Write-Host "  saving..."
        $doc.SaveAs([ref]$OutputPath, [ref]16)
        $doc.Close($false)
        Write-Host "  done."
    }
    finally {
        $word.Quit()
        [System.Runtime.InteropServices.Marshal]::ReleaseComObject($word) | Out-Null
    }
}

Build-Guide `
    -SourcePath  (Join-Path $DocsDir 'UserGuide-EndUser.md') `
    -OutputPath  (Join-Path $DocsDir 'UserGuide-EndUser.docx') `
    -FooterText  'BSM Assets Management - End-User Guide'

Build-Guide `
    -SourcePath  (Join-Path $DocsDir 'UserGuide-Admin.md') `
    -OutputPath  (Join-Path $DocsDir 'UserGuide-Admin.docx') `
    -FooterText  'BSM Assets Management - Administrator Guide'

Write-Host ""
Write-Host "All guides built."
