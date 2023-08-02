@echo off
cd "C:\Dev\MyProjects\RecklessSpeech"
dotnet publish -c Release -o ./backend_publish
cd "C:\Dev\MyProjects\RecklessSpeech\RecklessSpeech.Front\recklessspeech-front-electron-forge"
yarn make
start "C:\Dev\MyProjects\RecklessSpeech\RecklessSpeech.Front\recklessspeech-front-electron-forge\out\make\squirrel.windows\x64"
pause
