import Vue from 'vue'
import Vuex from 'vuex'
// import * as types from './mutation-types'
import notify from './notify'
import dialog from './dialog'
import company from './company'
import shop from './shop'
import field from './field'
import well from './well'

Vue.use(Vuex)

export default new Vuex.Store({
  plugins: [
    store => {
      store.subscribeAction((action, state) => {
        // store.commit(types.CLEAR_ERRORS)
        // store.commit(types.SET_LOADING, true)
      })
    }
  ],
  modules: {
    notify,
    dialog,
    company,
    shop,
    field,
    well
  }
})
