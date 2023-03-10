import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import vuetify from './plugins/vuetify'
import { loadFonts } from './plugins/webfontloader'
import Toast from "vue-toastification";
import "vue-toastification/dist/index.css";
const options = {
  // You can set your default options here
};

loadFonts()

createApp(App)
  .use(router)
  .use(vuetify)
  .use(Toast, options)
  .mount('#app')
