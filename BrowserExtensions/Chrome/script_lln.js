var clickedWords = new Set(); // Utilise un Set pour une recherche plus rapide

// Cette fonction déclenche un clic droit sur un élément
function triggerRightClick(element) {
  var rightClick = new MouseEvent('contextmenu', {
    bubbles: true,
    cancelable: true,
    button: 2 // bouton droit
  });
  element.dispatchEvent(rightClick);
}

// Vérifie si le mot doit être cliqué
function shouldClickWord(element, color) {
  const wordKey = element.getAttribute("data-word-key");
  const styleValue = element.style.color; // Utilise directement style.color pour la couleur
  return styleValue === color && !clickedWords.has(wordKey);
}

// Traite les mots d'une sélection spécifique
function processWords(selector, color) {
  document.querySelectorAll(selector).forEach(element => {
    if (shouldClickWord(element, color)) {
      triggerRightClick(element);
      clickedWords.add(element.getAttribute("data-word-key"));
    }
  });
}

function clickOnSubtitleWords() {
  // Process words for Language Reactor
  processWords(".lln-word", "rgb(255, 189, 128)");

  // Process words for YouTube et Netflix
  processWords(".lln-word.lln-hover-tooltip.top", "#ffbd80");
}

// Vérifie si l'utilisateur est sur un site pris en charge et démarre l'intervalle
if (window.location.href.startsWith("https://www.netflix.com/watch/") ||
    window.location.href.startsWith("https://www.languagereactor.com/m/") ||
    window.location.href.startsWith("https://www.youtube.com/watch")
) {
  var interval = 1000;
  console.log('start clicking every ' + interval + ' ms...');
  var intervalId = setInterval(() => {
    console.log('Pour arrêter: clearInterval(' + intervalId + ')');
    clickOnSubtitleWords();
  }, interval);
}



