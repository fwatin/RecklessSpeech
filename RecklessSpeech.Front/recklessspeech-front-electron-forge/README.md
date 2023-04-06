# recklessspeech-front-electron-forge

## Project setup
```
npm install
```

### Compiles and hot-reloads for development
```
yarn start
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
