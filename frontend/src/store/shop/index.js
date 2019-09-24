import context from '../../api/shop'
import * as types from '../mutation-types'

const state = {
  shops: null
}

// actions
const actions = {
  async getAllShops ({ commit, dispatch }, payload) {
    context.getAllShops(payload).then((x) => {
      if (x.status === 200) {
        commit(types.RECIEVE_SHOPS, x.data)
      } else {
        dispatch('setErrors', x.response.data)
      }
    }).catch(x => {
      dispatch('setErrors', x.response.data)
    })
  },
  async createShop ({ dispatch }, payload) {
    return new Promise((resolve, reject) => {
      context.createShop(payload).then((x) => {
        if (x.status === 200) {
          resolve()
        } else {
          reject(x.response.data)
          dispatch('setErrors', x.response.data)
        }
      }).catch(x => {
        reject(x.response.data)
        dispatch('setErrors', x.response.data)
      })
    })
  },
  async editShop ({ dispatch }, payload) {
    return new Promise((resolve, reject) => {
      context.editShop(payload).then((x) => {
        if (x.status === 200) {
          resolve()
        } else {
          console.log(x.response.data)
          dispatch('setErrors', 'Ошибка при сохранеии данных')
          reject(x.response.data)
        }
      }).catch(x => {
        console.log(x.response.data)
        dispatch('setErrors', 'Ошибка при сохранеии данных')
        reject(x.response.data)
      })
    })
  },
  async deleteShop ({ commit, dispatch }, payload) {
    context.deleteShop(payload).then((x) => {
      if (x.status === 200) {
        commit(types.DELETE_SHOP, payload)
      } else {
        dispatch('setErrors', x.response.data)
      }
    }).catch(x => {
      dispatch('setErrors', x.response.data)
    })
  }
}

// mutations
const mutations = {
  [types.RECIEVE_SHOPS] (state, payload) {
    state.shops = payload
  },
  [types.DELETE_SHOP] (state, payload) {
    let find = state.shops.indexOf(payload)
    if (find !== -1) {
      state.shops.splice(find, 1)
    }
  }
}

// getters
const getters = {
  getShopById: state => id => {
    if (state.shops !== null && state.shops !== undefined && state.shops !== 'undefined') {
      return state.shops.find(x => Number(x.id) === Number(id))
    } else {
      return null
    }
  }
}

export default {
  state,
  getters,
  actions,
  mutations
}
