import context from '../../api/field'
import * as types from '../mutation-types'

const state = {
  fields: null
}

// actions
const actions = {
  async getAllFields ({ commit, dispatch }, payload) {
    context.getAllFields(payload).then((x) => {
      if (x.status === 200) {
        commit(types.RECIEVE_FIELDS, x.data)
      } else {
        dispatch('setErrors', x.response.data)
      }
    }).catch(x => {
      dispatch('setErrors', x.response.data)
    })
  },
  async createField ({ dispatch }, payload) {
    return new Promise((resolve, reject) => {
      context.createField(payload).then((x) => {
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
  async editField ({ dispatch }, payload) {
    return new Promise((resolve, reject) => {
      context.editField(payload).then((x) => {
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
  async deleteField ({ commit, dispatch }, payload) {
    context.deleteField(payload).then((x) => {
      if (x.status === 200) {
        commit(types.DELETE_FIELD, payload)
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
  [types.RECIEVE_FIELDS] (state, payload) {
    state.fields = payload
  },
  [types.DELETE_FIELD] (state, payload) {
    let find = state.fields.indexOf(payload)
    if (find !== -1) {
      state.fields.splice(find, 1)
    }
  }
}

// getters
const getters = {
  getFieldById: state => id => {
    if (state.fields !== null && state.fields !== undefined && state.fields !== 'undefined') {
      return state.fields.find(x => Number(x.id) === Number(id))
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
