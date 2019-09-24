import axios from 'axios'
import config from '../init/config'

const Axios = axios.create({
  baseURL: config.apiAddress,
  headers: {
    // Accept: 'application/json',
    // 'Content-Type': 'application/json',
    // 'X-Requested-With': 'XMLHttpRequest',
  }
})

Axios.interceptors.request.use((config) => {
  return config
})

Axios.interceptors.response.use(response => {
  return response
}, error => {
  return error
})

export default Axios
