import context from '../../api/company'
// import Vue from 'vue'
import * as types from '../mutation-types'

const state = {
  companies: null
}

// actions
const actions = {
  async getAllCompanies ({ commit, dispatch }, payload) {
    context.getAllCompanies(payload).then((x) => {
      if (x.status === 200) {
        commit(types.RECIEVE_COMPANIES, x.data)
      } else {
        dispatch('setErrors', x.response.data)
      }
    }).catch(x => {
      dispatch('setErrors', x.response.data)
    })
  },
  async createCompany ({ dispatch }, payload) {
    return new Promise((resolve, reject) => {
      context.createCompany(payload).then((x) => {
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
  async editCompany ({ dispatch }, payload) {
    return new Promise((resolve, reject) => {
      context.editCompany(payload).then((x) => {
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
  async deleteCompany ({ commit, dispatch }, payload) {
    context.deleteCompany(payload).then((x) => {
      if (x.status === 200) {
        commit(types.DELETE_COMPANY, payload)
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
  [types.RECIEVE_COMPANIES] (state, payload) {
    state.companies = payload
  },
  [types.DELETE_COMPANY] (state, payload) {
    let find = state.companies.indexOf(payload)
    if (find !== -1) {
      state.companies.splice(find, 1)
    }
  }
}

// getters
const getters = {
  getCompanyById: state => id => {
    if (state.companies !== null && state.companies !== undefined && state.companies !== 'undefined') {
      return state.companies.find(x => Number(x.id) === Number(id))
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
