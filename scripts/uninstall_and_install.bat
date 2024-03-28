cd ..

echo "===Desinstallation de l'actuelle reckless speech"
start "" "%LOCALAPPDATA%\recklessspeech_front_electron_forge\Update.exe" -uninstall
echo Attendez que la désinstallation se termine...
timeout /t 10 /nobreak

echo "===Installation de Reckless Speech"
cd RecklessSpeech.Front\recklessspeech-front-electron-forge\out\make\squirrel.windows\x64
start "" "recklessspeech-front-electron-forge-1.0.0 Setup.exe"

echo "===Fini"
set /p DummyName=Appuyez sur une touche pour continuer...