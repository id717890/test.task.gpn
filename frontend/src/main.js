import Vue from 'vue'
import './plugins/vuetify'
import './plugins/font-awesome'
import './init/components'
import '@fortawesome/fontawesome-free/css/all.css'
import App from './App.vue'
import router from './router'
import store from './store'
import './registerServiceWorker'
import AxiosConfig from './api/http-config'
import VueJsModal from 'vue-js-modal'
Vue.$http = AxiosConfig

Vue.config.productionTip = false
Vue.use(VueJsModal, { dynamic: true })

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
