# recklessspeech-front-electron-forge

## Project setup
```
npm install
```

### Compiles and hot-reloads for development
```
yarn start
```

#### useful for testing in production conditions and avoiding uninstall/reinstall each time the backend is modified:
publish with :
```
dotnet publish -c Release -o ./backend_publish
```

then remove backend in installed version:
```
 Remove-Item -Path "C:\Users\felix\AppData\Local\recklessspeech_front_electron_forge\app-1.0.0\resources\backend_publish" -Recurse -Force
```

then copy new backend into installed version:
```
Copy-Item -Path "D:\Dev\MyProjects\RecklessSpeech\backend_publish" -Destination "C:\Users\felix\AppData\Local\recklessspeech_front_electron_forge\app-1.0.0\resources\" -Recurse
```


### Compiles and minifies for production

If some changes have been done in the backend, you should republish it with the command:
```
dotnet publish -c Release -o ./backend_publish
```

It will generate a new backend into RecklessSpeech\backend_publish
This folder is then copied into the resources on the yarn make execution (see packagerConfig: { extraResource: [) in the front project)

Run the following command in order to regenerate the front including the back in resources:
```
yarn make
```

### Publish (not tried yet)
```
yarn publish
```

### Useful links
[Electron documentation](https://www.electronjs.org/docs/latest/).
[CSP problems ?](https://githubmemory.com/repo/electron-userland/electron-forge/issues/2331)
[Electron Forge documentation](https://www.electronforge.io/)
