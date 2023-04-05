// Since we declared the script as type=module in the HTML file,
// we can use ES Modules (adapted from the Vue 2 Introduction
// https://vuejs.org/v2/guide/#Declarative-Rendering

// Alternatively, omit the .min from the path for Vue debugging purposes.
import Vue from '../node_modules/vue/dist/vue.esm.browser.min.js';

const app = new Vue({
  el: '#app',
  data: {
    message: 'Hello Vue!'
  }
})