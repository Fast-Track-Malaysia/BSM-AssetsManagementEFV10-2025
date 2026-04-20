param(
    [string]$SourcePath = (Join-Path $PSScriptRoot 'UserGuide.md'),
    [string]$OutputPath = (Join-Path $PSScriptRoot 'UserGuide.docx')
)

$ErrorActionPreference = 'Stop'

if (-not (Test-Path $SourcePath)) {
    throw "Source markdown not found: $SourcePath"
}

if (Test-Path $OutputPath) {
    Remove-Item $OutputPath -Force
}

Write-Host "Reading $SourcePath"
$lines = Get-Content -LiteralPath $SourcePath -Encoding UTF8

Write-Host "Launching Word..."
$word = New-Object -ComObject Word.Application
$word.Visible = $false
$word.DisplayAlerts = 0   # wdAlertsNone

try {
    $doc = $word.Documents.Add()

    # Page margins (in points): 1 inch = 72
    $ps = $doc.PageSetup
    $ps.TopMargin    = 72
    $ps.BottomMargin = 72
    $ps.LeftMargin   = 72
    $ps.RightMargin  = 72

    # Default paragraph style
    $normal = $doc.Styles.Item('Normal')
    $normal.Font.Name = 'Calibri'
    $normal.Font.Size = 11

    $selection = $word.Selection

    function Flush-Paragraph {
        param($sel, $text, $style)
        if ([string]::IsNullOrWhiteSpace($text)) { return }
        $sel.Style = $doc.Styles.Item($style)
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
        $sel.TypeParagraph()
    }

    function Flush-Bullet {
        param($sel, $text)
        $sel.Style = $doc.Styles.Item('List Bullet')
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
        $sel.TypeParagraph()
    }

    function Flush-Numbered {
        param($sel, $text)
        $sel.Style = $doc.Styles.Item('List Number')
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
        $sel.TypeParagraph()
    }

    function Flush-Note {
        param($sel, $text)
        $sel.Style = $doc.Styles.Item('Quote')
        $sel.Font.Italic = $true
        $sel.TypeText($text)
        $sel.Font.Italic = $false
        $sel.TypeParagraph()
    }

    # Cover block — treat the first H1 + subtitle specially
    $isFirstH1 = $true
    $coverSubtitleHandled = $false

    $buffer = ''
    $bufferStyle = 'Normal'

    for ($i = 0; $i -lt $lines.Count; $i++) {
        $line = $lines[$i]

        # Horizontal rule — treat as a page break
        if ($line -match '^---\s*$') {
            if ($buffer) { Flush-Paragraph $selection $buffer $bufferStyle; $buffer = '' }
            $selection.InsertBreak(7)  # wdPageBreak
            continue
        }

        if ($line -match '^#\s+(.*)$') {
            if ($buffer) { Flush-Paragraph $selection $buffer $bufferStyle; $buffer = '' }
            $text = $Matches[1]
            if ($isFirstH1) {
                $selection.Style = $doc.Styles.Item('Title')
                $selection.TypeText($text)
                $selection.TypeParagraph()
                $isFirstH1 = $false
            } else {
                Flush-Paragraph $selection $text 'Heading 1'
            }
            continue
        }
        if ($line -match '^##\s+(.*)$') {
            if ($buffer) { Flush-Paragraph $selection $buffer $bufferStyle; $buffer = '' }
            Flush-Paragraph $selection $Matches[1] 'Heading 2'
            continue
        }
        if ($line -match '^###\s+(.*)$') {
            if ($buffer) { Flush-Paragraph $selection $buffer $bufferStyle; $buffer = '' }
            Flush-Paragraph $selection $Matches[1] 'Heading 3'
            continue
        }
        if ($line -match '^-\s+(.*)$') {
            if ($buffer) { Flush-Paragraph $selection $buffer $bufferStyle; $buffer = '' }
            Flush-Bullet $selection $Matches[1]
            continue
        }
        if ($line -match '^(\d+)\.\s+(.*)$') {
            if ($buffer) { Flush-Paragraph $selection $buffer $bufferStyle; $buffer = '' }
            Flush-Numbered $selection $Matches[2]
            continue
        }
        if ($line -match '^>\s*(.*)$') {
            if ($buffer) { Flush-Paragraph $selection $buffer $bufferStyle; $buffer = '' }
            Flush-Note $selection $Matches[1]
            continue
        }
        if ($line -match '^\*(.+)\*\s*$') {
            if ($buffer) { Flush-Paragraph $selection $buffer $bufferStyle; $buffer = '' }
            $selection.Font.Italic = $true
            $selection.TypeText($Matches[1])
            $selection.Font.Italic = $false
            $selection.TypeParagraph()
            continue
        }
        if ([string]::IsNullOrWhiteSpace($line)) {
            if ($buffer) { Flush-Paragraph $selection $buffer $bufferStyle; $buffer = '' }
            continue
        }

        # Handle the subtitle line under the cover title
        if (-not $coverSubtitleHandled -and -not $isFirstH1 -and $buffer -eq '') {
            $selection.Style = $doc.Styles.Item('Subtitle')
            $selection.TypeText($line)
            $selection.TypeParagraph()
            $coverSubtitleHandled = $true
            continue
        }

        # Continuation text — append to buffer
        if ($buffer) {
            $buffer = "$buffer $line"
        } else {
            $buffer = $line
            $bufferStyle = 'Normal'
        }
    }
    if ($buffer) { Flush-Paragraph $selection $buffer $bufferStyle }

    # Insert table of contents at the top
    Write-Host "Inserting table of contents..."
    $tocStart = $doc.Range(0, 0)
    $tocStart.InsertParagraphBefore()
    $tocStart = $doc.Range(0, 0)
    $tocStart.Style = $doc.Styles.Item('Heading 1')
    $tocStart.Text = "Table of Contents`r`n"
    $tocRange = $doc.Range($tocStart.End, $tocStart.End)
    $doc.TablesOfContents.Add($tocRange, $true, 1, 3, $false) | Out-Null

    # Page break after TOC
    $afterToc = $doc.TablesOfContents.Item(1).Range
    $breakRange = $doc.Range($afterToc.End, $afterToc.End)
    $breakRange.InsertBreak(7)

    # Header/footer
    Write-Host "Adding header/footer..."
    foreach ($section in $doc.Sections) {
        $footer = $section.Footers.Item(1)   # wdHeaderFooterPrimary
        $footer.Range.Text = "BSM Assets Management - User Guide"
        $footer.Range.ParagraphFormat.Alignment = 1   # wdAlignParagraphCenter
        # Page number
        $pnRange = $doc.Range($footer.Range.End - 1, $footer.Range.End - 1)
        $pnRange.Text = "`t"
        $pnRange.Collapse(0)
        $footer.PageNumbers.Add(3, $false) | Out-Null   # wdAlignPageNumberRight
    }

    # Refresh TOC page numbers
    foreach ($toc in $doc.TablesOfContents) { $toc.Update() }

    Write-Host "Saving to $OutputPath"
    $doc.SaveAs([ref]$OutputPath, [ref]16)   # wdFormatDocumentDefault = 16 (.docx)
    $doc.Close($false)
}
finally {
    $word.Quit()
    [System.Runtime.InteropServices.Marshal]::ReleaseComObject($word) | Out-Null
}

Write-Host "Done: $OutputPath"
