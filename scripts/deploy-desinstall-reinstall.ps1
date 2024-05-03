# DÃ©finir le chemin du rÃ©pertoire actuel
$scriptDirectory = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $scriptDirectory
Set-Location ..

Write-Host "---Start publishing backend..."

# Passer au dossier de l'application .NET et le publier
# Masquer la sortie détaillée de dotnet publish
dotnet publish -c Release -o ./backend_publish > $null 2>&1
Write-Host "---Backend publishing finished"

# Passer au dossier de l'application Electron et le construire
Set-Location "RecklessSpeech.Front\recklessspeech-front-electron-forge"
# Exécuter yarn make et masquer les détails
Write-Host "---Start publishing front-end"
$null = yarn make
Write-Host "---Front-end publish finished"

Write-Host "---Start uninstallation of former version of Reckless speech"
Start-Process -FilePath "$env:LOCALAPPDATA\recklessspeech_front_electron_forge\Update.exe" -ArgumentList "-uninstall"
Write-Host "---Waiting for uninstalling to finish..."
Start-Sleep -Seconds 10
Write-Host "---uninstalling should be finished"

Write-Host "---Installation of Reckless Speech"
Set-Location "out\make\squirrel.windows\x64"
Start-Process -FilePath "recklessspeech-front-electron-forge-1.0.0 Setup.exe"

Write-Host "---installation is finished"
