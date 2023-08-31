@echo off
cd /d "%~dp0"

echo "Début de la publication..."
:: Passer au dossier de l'application .NET et le publier
dotnet publish -c Release -o ./backend_publish
echo "Publication terminée"
set /p DummyName=Appuyez sur une touche pour continuer...

:: Passer au dossier de l'application Electron et le construire
cd "RecklessSpeech.Front\recklessspeech-front-electron-forge"
yarn make

:: Lancer l'application Electron
start "out\make\squirrel.windows\x64"
pause

set /p DummyName=Appuyez sur une touche pour continuer...
