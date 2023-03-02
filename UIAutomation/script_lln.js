var clickedWords = [];

function ClickOnSubtitleWords() {
    console.log('start clicking...');

    var totalCount = document.querySelectorAll(".lln-word.lln-hover-tooltip.top").length;

    for (let i = 0; i < totalCount; i++) {
        var element = document.querySelectorAll(".lln-word.lln-hover-tooltip.top")[i];

        if (element.attributes["style"]?.value == "color: #ffbd80;" && !clickedWords.includes(element.attributes["data-word-key"]?.value)) {

            var event1 = new MouseEvent('contextmenu', {
                bubbles: true,
                cancelable: true,
                button: 2 // bouton droit
              });

              element.dispatchEvent(event1);
              clickedWords.push(element.attributes["data-word-key"]?.value);
              console.log('ajout du mot : ' + element.attributes["data-word-key"]?.value);
        }
    }
}

var i=0;
var intervalId = window.setInterval(function(){
  i=i+1;
  console.log('tour numéro: ' + i);
  console.log('Pour arrêter: clearInterval(' +intervalId +')');
  ClickOnSubtitleWords();
}, 1000);