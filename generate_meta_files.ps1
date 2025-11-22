# Unity Meta File Generator
# This script creates .meta files for all files in the package

function New-Guid {
    return [System.Guid]::NewGuid().ToString("N")
}

function Create-MetaFile {
    param(
        [string]$FilePath,
        [string]$FileType = "DefaultImporter"
    )
    
    $guid = New-Guid
    $metaPath = "$FilePath.meta"
    
    # Skip if .meta already exists
    if (Test-Path $metaPath) {
        Write-Host "Skipping existing: $metaPath" -ForegroundColor Yellow
        return
    }
    
    $metaContent = @"
fileFormatVersion: 2
guid: $guid
$FileType:
  externalObjects: {}
  userData: 
  assetBundleName: 
  assetBundleVariant: 
"@
    
    Set-Content -Path $metaPath -Value $metaContent -Encoding UTF8
    Write-Host "Created: $metaPath" -ForegroundColor Green
}

# Get all files except .meta files
$files = Get-ChildItem -Path . -Recurse -File | Where-Object { 
    -not $_.Name.EndsWith('.meta') -and 
    -not $_.Name.EndsWith('.ps1') -and
    -not $_.FullName.Contains('.git') -and
    -not $_.FullName.Contains('.vscode')
}

Write-Host "Found $($files.Count) files to process" -ForegroundColor Cyan

foreach ($file in $files) {
    $extension = $file.Extension.ToLower()
    
    switch ($extension) {
        ".cs" { 
            Create-MetaFile -FilePath $file.FullName -FileType "MonoImporter"
        }
        ".asmdef" { 
            Create-MetaFile -FilePath $file.FullName -FileType "AssemblyDefinitionImporter"
        }
        ".md" { 
            Create-MetaFile -FilePath $file.FullName -FileType "TextScriptImporter"
        }
        ".json" { 
            Create-MetaFile -FilePath $file.FullName -FileType "TextScriptImporter"
        }
        default { 
            Create-MetaFile -FilePath $file.FullName -FileType "DefaultImporter"
        }
    }
}

Write-Host "`nMeta file generation complete!" -ForegroundColor Green
