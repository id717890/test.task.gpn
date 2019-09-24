import Vue from 'vue'
export default {
  getAllWells: () => {
    return Vue.$http.get('api/well').then((x) => {
      return x
    }).catch(error => {
      return error
    })
  },
  createWell: (data) => {
    return Vue.$http.post('api/well/create', data).then((x) => {
      return x
    }).catch(error => {
      return error
    })
  },
  editWell: (data) => {
    return Vue.$http.post('api/well/edit', data).then((x) => {
      return x
    }).catch(error => {
      return error
    })
  },
  deleteWell: (data) => {
    return Vue.$http.post('api/well/delete', data).then((x) => {
      return x
    }).catch(error => {
      return error
    })
  },
  getAllWellTypes: () => {
    return Vue.$http.get('api/welltype').then((x) => {
      return x
    }).catch(error => {
      return error
    })
  }
}
