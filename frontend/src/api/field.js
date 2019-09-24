import Vue from 'vue'
export default {
  getAllFields: () => {
    return Vue.$http.get('api/field').then((x) => {
      return x
    }).catch(error => {
      return error
    })
  },
  createField: (data) => {
    return Vue.$http.post('api/field/create', data).then((x) => {
      return x
    }).catch(error => {
      return error
    })
  },
  editField: (data) => {
    return Vue.$http.post('api/field/edit', data).then((x) => {
      return x
    }).catch(error => {
      return error
    })
  },
  deleteField: (data) => {
    return Vue.$http.post('api/field/delete/', data).then((x) => {
      return x
    }).catch(error => {
      return error
    })
  }
}
