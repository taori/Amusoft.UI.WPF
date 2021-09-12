$configuration = "Release"
$verbosity = "n"

dotnet restore ../src/Amusoft.UI.WPF.All.sln --verbosity $verbosity
Write-Host "Restore complete" -ForegroundColor Green

dotnet build ../src/Amusoft.UI.WPF.All.sln --verbosity $verbosity -c $configuration --no-restore
Write-Host "Build complete" -ForegroundColor Green

dotnet test ../src/Amusoft.UI.WPF.All.sln --verbosity $verbosity -c $configuration --no-build 
Write-Host "Test complete" -ForegroundColor Green

dotnet pack ../src/Amusoft.UI.WPF/Amusoft.UI.WPF.csproj --verbosity $verbosity -c $configuration -o ../artifacts/nupkg --no-build /p:VersionSuffix=$versionSuffix
