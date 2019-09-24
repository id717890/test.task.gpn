import Vue from 'vue'
export default {
  getAllShops: () => {
    return Vue.$http.get('api/shop').then((x) => {
      return x
    }).catch(error => {
      return error
    })
  },
  createShop: (data) => {
    return Vue.$http.post('api/shop/create', data).then((x) => {
      return x
    }).catch(error => {
      return error
    })
  },
  editShop: (data) => {
    return Vue.$http.post('api/shop/edit', data).then((x) => {
      return x
    }).catch(error => {
      return error
    })
  },
  deleteShop: (data) => {
    return Vue.$http.post('api/shop/delete/', data).then((x) => {
      return x
    }).catch(error => {
      return error
    })
  }
}
