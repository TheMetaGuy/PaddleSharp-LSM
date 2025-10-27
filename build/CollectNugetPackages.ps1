# PowerShell script to collect all .nupkg and .snupkg files into build\localNuget
# this must be run from the build folder 

$solutionRoot = Split-Path $PSScriptRoot -Parent
$targetFolder = Join-Path $PSScriptRoot "localNuget"

# Create the target folder if it doesn't exist
if (-not (Test-Path $targetFolder)) {
    New-Item -ItemType Directory -Path $targetFolder | Out-Null
}

# Find all .nupkg and .snupkg files from the solution, excluding localNuget
Get-ChildItem -Path $solutionRoot -Recurse -Include *.nupkg,*.snupkg | Where-Object {
    $_.DirectoryName -ne $targetFolder
} | ForEach-Object {
    $fileName = $_.Name
    $targetPath = Join-Path $targetFolder $fileName

    # Delete existing package with the same name
    if (Test-Path $targetPath) {
        Remove-Item $targetPath -Force
    }

    # Copy the new package
    Copy-Item $_.FullName -Destination $targetFolder -Force
}

Write-Host "All .nupkg and .snupkg files have been copied to $targetFolder"
