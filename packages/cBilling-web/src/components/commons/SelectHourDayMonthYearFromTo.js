import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { push } from 'react-router-redux';
import { compose } from 'recompose';
import { MonthInput, YearInput, SelectInput, translate, DateInput } from 'ra-creportLib3';
import { connect } from 'react-redux';
import moment from 'moment-timezone';
const typeTime = [
  { id: 'hour', name: 'generic.typeTime.hour' },
  { id: 'day', name: 'generic.typeTime.day' },
  { id: 'month', name: 'generic.typeTime.month' },
  { id: 'year', name: 'generic.typeTime.year' },
];

class SelectHourDayMonthYearFromTo extends Component {
  constructor(props) {
    super(props);
    this.state = {
      type: 'month',
      monthFrom: moment(new Date())
        .subtract(6, 'month')
        .format('YYYY-MM'),
      monthTo: moment(new Date()).format('YYYY-MM'),
      yearFrom: '',
      yearTo: '',
      dayFrom: '',
      dayTo: '',
      dayHour: '',
    };
  }
  onTypeChange = (event, value) => {
    let from, to;
    switch (value) {
      case 'hour': {
        from = this.state.dayHour;
        to = this.state.dayHour;
        break;
      }
      case 'day': {
        from = this.state.dayFrom;
        to = this.state.dayTo;
        break;
      }
      case 'month': {
        from = this.state.monthFrom;
        to = this.state.monthTo;
        break;
      }
      case 'year': {
        from = this.state.yearFrom;
        to = this.state.yearTo;
        break;
      }
    }
    this.setState({
      type: value,
    });
    this.setTime(value, from, to);
  };
  setTime = (type, from, to) => {
    if (from && to && from > to) {
      let t = from;
      from = to;
      to = t;
    }
    let tmp = {};
    tmp.typeTime = type;
    tmp.valueTimeFrom = from;
    tmp.valueTimeTo = to;
    this.props.onChangeTime(tmp);
  };
  onChangeYearFrom = (e, val) => {
    this.setState({ yearFrom: val });
    this.setTime(this.state.type, val, this.state.yearTo);
  };
  onChangeYearTo = (e, val) => {
    this.setState({ yearTo: val });
    this.setTime(this.state.type, this.state.yearFrom, val);
  };
  onChangeMonthFrom = (e, val) => {
    this.setState({ monthFrom: val });
    this.setTime(this.state.type, val, this.state.monthTo);
  };
  onChangeMonthTo = (e, val) => {
    this.setState({ monthTo: val });
    this.setTime(this.state.type, this.state.monthFrom, val);
  };
  onChangeDayFrom = (e, val) => {
    this.setState({ dayFrom: val });
    this.setTime(this.state.type, val, this.state.dayTo);
  };
  onChangeDayTo = (e, val) => {
    this.setState({ dayTo: val });
    this.setTime(this.state.type, this.state.dayFrom, val);
  };
  onChangeHour = (e, val) => {
    this.setState({ dayHour: val });
    this.setTime(this.state.type, val, val);
  };
  renderInputType() {
    switch (this.state.type) {
      case 'hour': {
        return (
          <div style={{ float: 'left' }}>
            <DateInput
              label={this.props.translate('generic.typeTime.day')}
              source={this.props.sourceHour}
              onChange={this.onChangeHour}
              style={{ marginLeft: '10px' }}
            />
          </div>
        );
      }
      case 'day': {
        return (
          <div style={{ float: 'left' }}>
            <DateInput
              label={this.props.translate('generic.typeTime.dayFrom')}
              source={this.props.sourceDayFrom}
              onChange={this.onChangeDayFrom}
              style={{ marginLeft: '10px' }}
            />
            <DateInput
              label={this.props.translate('generic.typeTime.dayTo')}
              source={this.props.sourceDayTo}
              onChange={this.onChangeDayTo}
              style={{ marginLeft: '10px' }}
            />
          </div>
        );
      }
      case 'month': {
        return (
          <div style={{ float: 'left' }}>
            <MonthInput
              source={this.props.sourceMonthFrom}
              onChange={this.onChangeMonthFrom}
              label={this.props.translate('generic.typeTime.monthFrom')}
              style={{ marginLeft: '10px' }}
              defaultValue={moment(new Date())
                .subtract(6, 'month')
                .format('YYYY-MM')}
            />
            <MonthInput
              source={this.props.sourceMonthTo}
              onChange={this.onChangeMonthTo}
              label={this.props.translate('generic.typeTime.monthTo')}
              style={{ marginLeft: '10px' }}
              defaultValue={moment(new Date()).format('YYYY-MM')}
            />
          </div>
        );
      }
      case 'year': {
        return (
          <div style={{ float: 'left' }}>
            <YearInput
              source={this.props.sourceYearFrom}
              onChange={this.onChangeYearFrom}
              label={this.props.translate('generic.typeTime.yearFrom')}
              style={{ width: 50, marginLeft: '10px' }}
            />
            <YearInput
              source={this.props.sourceYearTo}
              onChange={this.onChangeYearTo}
              label={this.props.translate('generic.typeTime.yearTo')}
              style={{ width: 50, marginLeft: '10px' }}
            />
          </div>
        );
      }
      default: {
        return null;
      }
    }
  }
  render() {
    return (
      <div>
        <SelectInput
          onChange={this.onTypeChange}
          choices={typeTime}
          label={this.props.translate('generic.statistic.selectTime')}
          style={{ marginLeft: '5px', float: 'left' }}
          translateChoice={true}
          source={this.props.sourceTypeTime}
          defaultValue={'month'}
        />
        {this.renderInputType()}
      </div>
    );
  }
}

SelectHourDayMonthYearFromTo.defaultProps = {};

SelectHourDayMonthYearFromTo.propTypes = {
  onChangeTime: PropTypes.func,
  translate: PropTypes.func,
  sourceTypeTime: PropTypes.string.isRequired,
  sourceHour: PropTypes.string.isRequired,
  sourceDayFrom: PropTypes.string.isRequired,
  sourceDayTo: PropTypes.string.isRequired,
  sourceMonthFrom: PropTypes.string.isRequired,
  sourceMonthTo: PropTypes.string.isRequired,
  sourceYearFrom: PropTypes.string.isRequired,
  sourceYearTo: PropTypes.string.isRequired,
};
const enhance = compose(
  translate,
  connect(null, {
    push,
  }),
);

export default enhance(SelectHourDayMonthYearFromTo);
