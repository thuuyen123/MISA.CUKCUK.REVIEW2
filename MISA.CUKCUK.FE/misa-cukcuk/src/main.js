import Vue from 'vue'
import axios from 'axios'
import VueAxios from 'vue-axios'
import Toast from "vue-toastification";
import App from './App.vue'
import router from './router'
import store from './store'
import "vue-toastification/dist/index.css";
require("./js/common/prototype");
Vue.config.productionTip = false



export const eventBus = new Vue();
Vue.use(Toast, {
  transition: "Vue-Toastification__bounce",
  maxToasts: 10,
  newestOnTop: true
}); 

Vue.use(VueAxios, axios)
new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
