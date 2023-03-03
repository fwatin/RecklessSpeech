var clickedWords = [];

function ClickOnSubtitleWords() {
    var totalCount = document.querySelectorAll(".lln-word.lln-hover-tooltip.top").length;

    for (let i = 0; i < totalCount; i++) {
        var element = document.querySelectorAll(".lln-word.lln-hover-tooltip.top")[i];

        if (element.attributes["style"]?.value == "color: #ffbd80;" && !clickedWords.includes(element.attributes["data-word-key"]?.value)) {

            var rightClick = new MouseEvent('contextmenu', {
                bubbles: true,
                cancelable: true,
                button: 2 // bouton droit
              });

              element.dispatchEvent(rightClick);
              clickedWords.push(element.attributes["data-word-key"]?.value);
              console.log('ajout du mot : ' + element.attributes["data-word-key"]?.value);
        }
    }
}

if (window.location.href.startsWith("https://www.netflix.com/watch/")) {
  var interval = 1000;
  console.log('start clicking every ' + interval + ' ms...');
  var intervalId = window.setInterval(function(){
    console.log('Pour arrêter: clearInterval(' +intervalId +')');
    ClickOnSubtitleWords();
  }, 1000);
}

