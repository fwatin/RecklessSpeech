const { webFrame } = require('electron');

webFrame.registerURLSchemeAsPrivileged('http', {
  bypassCSP: true,
  secure: false,
  supportFetchAPI: true,
  allowServiceWorkers: false,
  corsEnabled: true,
});

webFrame.executeJavaScript(`
  document.addEventListener('DOMContentLoaded', () => {
    const csp = document.createElement('meta');
    csp.setAttribute('http-equiv', 'Content-Security-Policy');
    csp.setAttribute('content', "default-src 'self' 'unsafe-inline' data:; connect-src 'self' https://localhost:47973; script-src 'self' 'unsafe-inline' 'unsafe-eval';");
    document.head.appendChild(csp);
  });
`);
