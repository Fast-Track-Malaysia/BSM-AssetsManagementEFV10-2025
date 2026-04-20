param(
    [string]$OutDir = (Join-Path $PSScriptRoot 'screenshots')
)

Add-Type -AssemblyName System.Drawing

if (-not (Test-Path $OutDir)) {
    New-Item -ItemType Directory -Path $OutDir | Out-Null
}

$shots = @(
    @{ Name = 'login';               Caption = 'Login screen' }
    @{ Name = 'nav-pane';             Caption = 'Navigation pane (left side)' }
    @{ Name = 'list-view';            Caption = 'Typical list view with toolbar' }
    @{ Name = 'detail-view';          Caption = 'Typical detail view with tabs' }
    @{ Name = 'equipment-list';       Caption = 'Equipments list view' }
    @{ Name = 'equipment-detail';     Caption = 'Equipment detail view' }
    @{ Name = 'location-setup';       Caption = 'Areas / Locations / Sub Locations' }
    @{ Name = 'eq-class';             Caption = 'Equipment Class detail view' }
    @{ Name = 'pm-schedule';          Caption = 'PM Schedule detail view' }
    @{ Name = 'pm-patch';             Caption = 'PM Patch — generate action' }
    @{ Name = 'wr-new';               Caption = 'New Work Request' }
    @{ Name = 'wr-detail';            Caption = 'Work Request detail view' }
    @{ Name = 'wo-header';            Caption = 'Work Order — Header tab' }
    @{ Name = 'wo-operations';        Caption = 'Work Order — Operations tab' }
    @{ Name = 'wo-manhours';          Caption = 'Work Order — Man Hours tab' }
    @{ Name = 'wo-jobstatus';         Caption = 'Work Order — Job Status tab' }
    @{ Name = 'pr-from-wo';           Caption = 'Creating a PR from a Work Order' }
    @{ Name = 'pr-detail';            Caption = 'Purchase Request detail view' }
    @{ Name = 'pr-post-sap';          Caption = 'Post PR to SAP B1 action' }
    @{ Name = 'deviation-new';        Caption = 'New Deviation' }
    @{ Name = 'deviation-risk';       Caption = 'Deviation — Risk Assessment tabs' }
    @{ Name = 'deviation-workflow';   Caption = 'Deviation approval workflow actions' }
    @{ Name = 'report-list';          Caption = 'Reports navigation group' }
    @{ Name = 'report-preview';       Caption = 'Report preview window' }
    @{ Name = 'admin-users';          Caption = 'User administration screen' }
    @{ Name = 'admin-role';           Caption = 'Role assignment' }
    @{ Name = 'admin-report-upload';  Caption = 'Uploading a new .repx report' }
    @{ Name = 'admin-webconfig';      Caption = 'Web.config key configuration area' }
    @{ Name = 'job-status-setup';     Caption = 'Job Statuses master data' }
    @{ Name = 'contractor-setup';     Caption = 'Contractor and Contract Document setup' }
)

$width  = 1200
$height = 700
$border = 4

foreach ($s in $shots) {
    $path = Join-Path $OutDir ($s.Name + '.png')
    if (Test-Path $path) {
        Write-Host "skip   $($s.Name).png (already exists)"
        continue
    }

    $bmp = New-Object System.Drawing.Bitmap $width, $height
    $g = [System.Drawing.Graphics]::FromImage($bmp)
    $g.SmoothingMode = 'AntiAlias'
    $g.TextRenderingHint = 'AntiAliasGridFit'

    $bg = [System.Drawing.Color]::FromArgb(240, 244, 248)
    $bgBrush = New-Object System.Drawing.SolidBrush $bg
    $g.FillRectangle($bgBrush, 0, 0, $width, $height)

    $borderPen = New-Object System.Drawing.Pen ([System.Drawing.Color]::FromArgb(120, 140, 160)), $border
    $g.DrawRectangle($borderPen, $border / 2, $border / 2, $width - $border, $height - $border)

    # Header bar (mimic app title bar)
    $header = [System.Drawing.Color]::FromArgb(46, 90, 136)
    $headerBrush = New-Object System.Drawing.SolidBrush $header
    $g.FillRectangle($headerBrush, $border, $border, $width - 2 * $border, 60)

    $headerFont = New-Object System.Drawing.Font 'Segoe UI', 14, ([System.Drawing.FontStyle]::Bold)
    $white = New-Object System.Drawing.SolidBrush ([System.Drawing.Color]::White)
    $g.DrawString('BSM Assets Management', $headerFont, $white, 20, 20)

    # Filename label bottom-right
    $smallFont = New-Object System.Drawing.Font 'Consolas', 10
    $grey = New-Object System.Drawing.SolidBrush ([System.Drawing.Color]::FromArgb(100, 110, 120))
    $g.DrawString("[placeholder: $($s.Name).png]", $smallFont, $grey, 20, $height - 28)

    # Centered caption
    $titleFont = New-Object System.Drawing.Font 'Segoe UI', 24, ([System.Drawing.FontStyle]::Regular)
    $dark = New-Object System.Drawing.SolidBrush ([System.Drawing.Color]::FromArgb(40, 60, 80))
    $size = $g.MeasureString($s.Caption, $titleFont)
    $x = ($width - $size.Width) / 2
    $y = ($height - $size.Height) / 2
    $g.DrawString($s.Caption, $titleFont, $dark, $x, $y)

    $instrFont = New-Object System.Drawing.Font 'Segoe UI', 11, ([System.Drawing.FontStyle]::Italic)
    $instr = 'Replace this file with a real screenshot and rebuild the guide.'
    $sizeI = $g.MeasureString($instr, $instrFont)
    $g.DrawString($instr, $instrFont, $grey, ($width - $sizeI.Width) / 2, $y + $size.Height + 20)

    $g.Dispose()
    $bmp.Save($path, [System.Drawing.Imaging.ImageFormat]::Png)
    $bmp.Dispose()
    Write-Host "create $($s.Name).png"
}

Write-Host "Done. $($shots.Count) placeholders under $OutDir"
