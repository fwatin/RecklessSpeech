@echo off
cd /d "%~dp0"
cd ..

echo "Début de la publication..."
:: Passer au dossier de l'application .NET et le publier
dotnet publish -c Release -o ./backend_publish
echo "----------------------------------------Publish terminée"

:: Passer au dossier de l'application Electron et le construire
cd "RecklessSpeech.Front\recklessspeech-front-electron-forge"
yarn make
echo "----------------------------------------Build de l'install terminée"

:: Pause pour voir le résultat
set /p DummyName=Appuyez sur une touche pour continuer...
