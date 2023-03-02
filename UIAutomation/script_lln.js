function ClickOnSubtitleWords() {
    console.log('start clicking...');

    var totalCount = document.querySelectorAll(".lln-word.lln-hover-tooltip.top").length;

    for (let i = 0; i < totalCount; i++) {
        var element = document.querySelectorAll(".lln-word.lln-hover-tooltip.top")[i];

        if (element.attributes["style"]?.value == "color: #ffbd80;") {

            var event1 = new MouseEvent('contextmenu', {
                bubbles: true,
                cancelable: true,
                button: 2 // bouton droit
              });

              var event2 = new MouseEvent('contextmenu', {
                bubbles: true,
                cancelable: true,
                button: 2 // bouton droit
              });
              element.dispatchEvent(event1);
              console.log('premier clic droit');
               setTimeout(() => {
               }, 100);
               element.dispatchEvent(event2);
               console.log('deuxieme clic droit');
        }
    }
}

// var i=0;
// var intervalId = window.setInterval(function(){
//   i=i+1;
  console.log('tour numéro: ' + i);
  console.log('Pour arrêter: clearInterval(' +intervalId +')');
  ClickOnSubtitleWords();
//}, 2000);