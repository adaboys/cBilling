import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { compose } from 'recompose';
import { FlexFormFilter, withDataProvider, translate, MonthInput } from 'ra-creportLib3';
import { Grid, withStyles } from '@material-ui/core';
import { connect } from 'react-redux';
import moment from 'moment-timezone';
const styles = () => ({
  condition: {
    margin: 0,
    padding: '0 !important',
  },
});
class FilterFromToTime extends Component {
  formRef = React.createRef();
  onChangeFilter = filter => {
    let { fromTime, toTime } = filter;
    if (fromTime && toTime && fromTime > toTime) {
      let t = fromTime;
      fromTime = toTime;
      toTime = t;
    }
    let tmp = {
      fromTime,
      toTime,
    };
    this.props.getFilter(tmp);
  };
  render() {
    let { formName, defautFromTime, defautToTime, translate } = this.props;
    return (
      <Grid container>
        <Grid item xs={12}>
          <FlexFormFilter
            formName={formName}
            onChange={this.onChangeFilter}
            formRef={this.formRef}
            defaultValue={{
              fromTime: defautFromTime,
              toTime: defautToTime,
            }}
          >
            <Grid middle container>
              <MonthInput
                date
                source={'fromTime'}
                label={translate('generic.typeTime.monthFrom')}
                style={{ marginLeft: '10px', width: '130px' }}
              />
              <MonthInput
                date
                source={'toTime'}
                label={translate('generic.typeTime.monthTo')}
                style={{ marginLeft: '10px', width: '130px' }}
              />
            </Grid>
          </FlexFormFilter>
        </Grid>
      </Grid>
    );
  }
}
// eslint-disable-next-line
const mapStateToProps = state => {
  return {};
};
FilterFromToTime.defaultProps = {
  formName: 'formFilterFromToTime',
  defautFromTime: moment()
    .subtract(6, 'month')
    .toDate(),
  defautToTime: moment().toDate(),
};
FilterFromToTime.propTypes = {
  title: PropTypes.string,
  classes: PropTypes.object,
  dataProvider: PropTypes.func,
  translate: PropTypes.func,
  dispatch: PropTypes.any,
  getFilter: PropTypes.func,
  formName: PropTypes.string,
  defautFromTime: PropTypes.any,
  defautToTime: PropTypes.any,
};
const enhance = compose(translate, withStyles(styles), withDataProvider, connect(mapStateToProps));
export default enhance(FilterFromToTime);
