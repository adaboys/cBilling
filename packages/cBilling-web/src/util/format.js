const numeral = require('numeral');
const moment = require('moment-timezone');

const postfix = decimal => {
  let postfix = '';
  if (decimal > 0) {
    postfix += '.';
    for (let i = 0; i < decimal; i++) {
      postfix += '0';
    }
  }
  return postfix;
};

const format = {
  geo: data => {
    return `${numeral(data.lat).format('0.00000')},${numeral(data.lng).format('0.00000')}`;
  },
  number: (data, decimal = 0) => {
    return numeral(data).format(`0,0${postfix(decimal)}`);
  },
  division: (a, b, decimal = 0) => {
    return format.number(a / b, decimal);
  },
  numberWithUnit: (data, unit, decimal = 0) => {
    return `${format.number(data, decimal)} ${unit}`;
  },
  currency: (data, decimal = 0) => {
    return numeral(data).format(`0,0${postfix(decimal)} $`);
  },
  getPercentValue: (data, factor = 100) => {
    return (data * factor).toFixed(2);
  },
  getPercentAbValue: (a, b, factor = 100) => {
    return ((a * factor) / b).toFixed(2);
  },
  percent: (data, factor = 100, decimal = 0) => {
    return `${format.number(data * factor, decimal)}%`;
  },
  percentAb: (a, b, factor = 100, decimal = 0) => {
    return `${format.number((a * factor) / b, decimal)}%`;
  },
  percentP: (data, factor = 100, decimal = 0) => {
    return `(${format.percent(data, factor, decimal)})`;
  },
  percentPab: (a, b, factor = 100, decimal = 0) => {
    return `(${format.percentAb(a, b, factor, decimal)})`;
  },
  getPercentP: a => {
    return `(${a}%)`;
  },
  shortDate: (data, noData = '-') => {
    if (data) {
      return moment(data).format('DD/MM/YYYY');
    }
    return noData;
  },
  shortTime: (data, noData = '-') => {
    if (data) {
      return moment(data).format('HH:mm');
    }
    return noData;
  },
  shortDateTime: (data, noData = '-') => {
    if (data) {
      return moment(data).format('DD/MM HH:mm');
    }
    return noData;
  },
  formatDatePicker: date => {
    return date.getDate() + '-' + (date.getMonth() + 1) + '-' + date.getFullYear();
  },

  // 11.618333333333334 => 11.62
  // 365 => 365
  formatWithDec: (data, decimal = 0, defaultRet = '') => {
    if (typeof data === 'undefined' || data === '' || data === null) return defaultRet;
    return parseFloat(data.toFixed(decimal));
  },

  formatNumber: (data, decimal = 0, defaultRet = '') => {
    if (typeof data === 'undefined' || data === '' || data === null || isNaN(data)) return defaultRet;
    return format.number(data, decimal);
  },
};

module.exports = format;
