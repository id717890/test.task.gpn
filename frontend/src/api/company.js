import Vue from 'vue'
export default {
  getAllCompanies: () => {
    return Vue.$http.get('api/company').then((x) => {
      return x
    }).catch(error => {
      return error
    })
  },
  createCompany: (data) => {
    return Vue.$http.post('api/company/create', data).then((x) => {
      return x
    }).catch(error => {
      return error
    })
  },
  editCompany: (data) => {
    return Vue.$http.post('api/company/edit', data).then((x) => {
      return x
    }).catch(error => {
      return error
    })
  },
  deleteCompany: (data) => {
    return Vue.$http.post('api/company/delete/', data).then((x) => {
      return x
    }).catch(error => {
      return error
    })
  }
}
