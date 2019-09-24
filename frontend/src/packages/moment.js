import moment from 'moment'

export default {
  newTimestamp () {
    return moment().format('YYYY-MM-DD h:mm:ss')
  },
  getDate (format) {
    return moment().format(format)
  },
  getDateFormat (date, format) {
    // console.log(moment(date))
    return moment(date).format(format)
  }
}
