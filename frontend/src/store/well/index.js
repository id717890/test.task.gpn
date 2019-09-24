import context from '../../api/well'
import * as types from '../mutation-types'

const state = {
  wells: null,
  wellTypes: null
}

// actions
const actions = {
  async getAllWellTypes ({ commit, dispatch }, payload) {
    context.getAllWellTypes(payload).then((x) => {
      if (x.status === 200) {
        commit(types.RECIEVE_WELL_TYPES, x.data)
      } else {
        dispatch('setErrors', x.response.data)
      }
    }).catch(x => {
      dispatch('setErrors', x.response.data)
    })
  },
  async getAllWells ({ commit, dispatch }, payload) {
    context.getAllWells(payload).then((x) => {
      if (x.status === 200) {
        commit(types.RECIEVE_WELLS, x.data)
      } else {
        dispatch('setErrors', x.response.data)
      }
    }).catch(x => {
      dispatch('setErrors', x.response.data)
    })
  },
  async createWell ({ dispatch }, payload) {
    return new Promise((resolve, reject) => {
      context.createWell(payload).then((x) => {
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
  async editWell ({ dispatch }, payload) {
    return new Promise((resolve, reject) => {
      context.editWell(payload).then((x) => {
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
  async deleteWell ({ commit, dispatch }, payload) {
    context.deleteWell(payload).then((x) => {
      if (x.status === 200) {
        commit(types.DELETE_WELL, payload)
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
  [types.RECIEVE_WELLS] (state, payload) {
    state.wells = payload
  },
  [types.RECIEVE_WELL_TYPES] (state, payload) {
    state.wellTypes = payload
  },
  [types.DELETE_WELL] (state, payload) {
    let find = state.wells.indexOf(payload)
    if (find !== -1) {
      state.wells.splice(find, 1)
    }
  }
}

// getters
const getters = {
  getWellById: state => id => {
    if (state.wells !== null && state.wells !== undefined && state.wells !== 'undefined') {
      return state.wells.find(x => Number(x.id) === Number(id))
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
