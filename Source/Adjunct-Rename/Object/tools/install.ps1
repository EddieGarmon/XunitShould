param($installPath, $toolsPath, $package, $project)

Write-Host "Finishing Rename"

Uninstall-Package -Id $package.Id -ProjectName $project.Name -Force -Version $package.Version
