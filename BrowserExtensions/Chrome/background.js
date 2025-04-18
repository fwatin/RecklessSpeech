chrome.runtime.onMessage.addListener((message, sender, sendResponse) => {
  if (message.action === "setIconGreen") {
    chrome.action.setIcon({
      path: {
        16: "icons/icon-green-16.png",
        32: "icons/icon-green-32.png",
        48: "icons/icon-green-48.png",
        128: "icons/icon-green-128.png"
      }
    });
  }

  // Optionnel : remettre l'icône bleue
  else if (message.action === "setIconBlue") {
    chrome.action.setIcon({
      path: {
        16: "icons/icon-blue-16.png",
        32: "icons/icon-blue-32.png",
        48: "icons/icon-blue-48.png",
        128: "icons/icon-blue-128.png"
      }
    });
  }
});
