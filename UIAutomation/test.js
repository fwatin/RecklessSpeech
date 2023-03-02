function ClickOnSubtitleWords() {
    console.log('start clicking...');

    var totalCount = document.querySelectorAll(".lln-word.lln-hover-tooltip.top").length;

    for (let i = 0; i < totalCount; i++) {
        var element = document.querySelectorAll(".lln-word.lln-hover-tooltip.top")[i];

        console.log('attribute of element: ' + i);
        console.log(element.attributes);
        console.log('style is '+ element.attributes["style"]?.value);

         if (element.attributes["style"]?.value == "color: #ffbd80;") {
             console.log('inside color orange with element ' + i);
         }
    }
}
  ClickOnSubtitleWords();