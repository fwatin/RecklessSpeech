const { app, BrowserWindow } = require('electron');
const path = require('path');
const { spawn } = require('child_process');

// Handle creating/removing shortcuts on Windows when installing/uninstalling.
if (require('electron-squirrel-startup')) {
  app.quit();
}

const createWindow = () => {
  // Create the browser window.
  const mainWindow = new BrowserWindow({
    width: 800,
    height: 600,
    webPreferences: {
      nodeIntegration: true,
    },
  });

  if (process.env.NODE_ENV === 'production') {
    // Chemin vers le fichier .exe du backend
    const backendPath = path.join(process.resourcesPath, 'backend_publish\\RecklessSpeech.Web.exe');
    
    // Options pour le processus fils
    const options = {
      cwd: path.join(process.resourcesPath, 'backend_publish'), // Ici, nous définissons le répertoire de travail
      detached: true,
      stdio: ['ignore', 'inherit', 'inherit'],
      shell: true
    };
    
    
    // Lance le processus du backend
    const backendProcess = spawn(backendPath, [], options);
    
    // Détache le processus fils
    backendProcess.unref();
    
    // Tue le processus du backend lorsque la fenêtre principale est fermée
    mainWindow.on('closed', () => {
      backendProcess.kill();
    });
  }
  

  // and load the index.html of the app.
  mainWindow.loadURL(MAIN_WINDOW_WEBPACK_ENTRY);

  // Open the DevTools.
  if (process.env.NODE_ENV === 'development') {
    mainWindow.webContents.openDevTools();
  }

};

// This method will be called when Electron has finished
// initialization and is ready to create browser windows.
// Some APIs can only be used after this event occurs.
app.on('ready', createWindow);

// Quit when all windows are closed, except on macOS. There, it's common
// for applications and their menu bar to stay active until the user quits
// explicitly with Cmd + Q.
app.on('window-all-closed', () => {
  if (process.platform !== 'darwin') {
    app.quit();
  }
});

app.on('activate', () => {
  // On OS X it's common to re-create a window in the app when the
  // dock icon is clicked and there are no other windows open.
  if (BrowserWindow.getAllWindows().length === 0) {
    createWindow();
  }
});

// In this file you can include the rest of your app's specific main process
// code. You can also put them in separate files and import them here.
