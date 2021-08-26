[CmdletBinding()]
param(
    [Parameter(Position = 0)]
    [ValidateSet(
        "Debug",
        "Release"
    )]
    [string]$ConfigType = "Debug"
)

$scriptRoot = $PSScriptRoot

$buildDir = Join-Path -Path $scriptRoot -ChildPath "build"
$builtModuleDir = Join-Path -Path $buildDir -ChildPath "PasswordGenerator"

$slnFile = Join-Path -Path $scriptRoot -ChildPath "pwsh-rng-password.sln"

$filesToCopy = @(
    (Join-Path -Path $scriptRoot -ChildPath "src\PasswordGenerator\bin\$($ConfigType)\net5.0\publish\PasswordGenerator.dll"), #Compile module assembly file
    (Join-Path -Path $scriptRoot -ChildPath "src\PasswordGenerator.Module\PasswordGenerator.psd1") #Module manifest
)
if (Test-Path -Path $buildDir) {
    Remove-Item -Path $buildDir -Recurse -Force
}
$null = New-Item -Path $buildDir -ItemType "Directory"
$builtModuleDirObj = New-Item -Path $builtModuleDir -ItemType "Directory"

dotnet clean $slnFile
dotnet publish $slnFile --configuration $ConfigType

foreach ($item in $filesToCopy) {
    Copy-Item -Path $item -Destination $builtModuleDirObj.FullName -Force   
}