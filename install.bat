:: Demander à l'utilisateur s'il souhaite installer l'application
cd "RecklessSpeech.Front\recklessspeech-front-electron-forge"
cd "out\make\squirrel.windows\x64"
set /p UserChoice=Voulez-vous installer l'application ? (O/N) : 

:: Vérifier le choix de l'utilisateur
if /I "%UserChoice%"=="O" (
    echo Installation en cours...
    start "recklessspeech-front-electron-forge-1.0.0 Setup.exe"
    echo "----------------------------------------Installation terminée"
) else if /I "%UserChoice%"=="N" (
    echo Pas d'installation.
) else (
    echo Choix non valide.
)

:: Pause pour voir le résultat
set /p DummyName=Appuyez sur une touche pour continuer...
