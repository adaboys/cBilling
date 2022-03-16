import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { compose } from 'recompose';
import {
  FlexFormFilter,
  withDataProvider,
  translate,
  ReferenceInput,
  SelectInput,
  MonthInput,
  CUSTOM,
} from 'ra-creportLib3';
import { Grid, withStyles } from '@material-ui/core';
import { connect } from 'react-redux';
import moment from 'moment-timezone';
const styles = () => ({
  condition: {
    margin: 0,
    padding: '0 !important',
  },
});
class FilterTimeGeo extends Component {
  formRef = React.createRef();
  state = {
    provinceId: '',
    districtId: '',
  };
  componentDidMount() {
    // set default
    this.props.dataProvider(CUSTOM, 'geoprovinces', {}).then(res => {
      if (res.data && res.data.length > 0) {
        this.formRef.current.props.change('provinceId', res.data[0].id);
        this.onChangeProvince('', res.data[0].id);
      } else {
        this.formRef.current.props.change('provinceId', '');
        this.onChangeProvince('', '');
      }
    });
    this.formRef.current.props.change('time', moment().toDate());
    // this.formRef.current.props.change('districtId', '');
    // this.formRef.current.props.change('wardId', '');
  }
  // eslint-disable-next-line
  onChangeProvince = (e, val) => {
    this.setState({ provinceId: val, districtId: '' });
    this.formRef.current.props.change('districtId', '');
    this.formRef.current.props.change('wardId', '');
  };
  // eslint-disable-next-line
  onChangeDistrict = (e, val) => {
    this.setState({ districtId: val });
    this.formRef.current.props.change('wardId', '');
  };
  onChangeFilter = filter => {
    this.props.getFilter(filter);
  };
  render() {
    // console.log('this.state', this.props);
    return (
      <Grid container>
        <Grid item xs={12}>
          <FlexFormFilter
            onChange={this.onChangeFilter}
            formName={this.props.formName}
            resource="clients"
            formRef={this.formRef}
            style={{ marginTop: '9px' }}
            defaultValue={{
              time: moment().toDate(),
              provinceId: '',
              districtId: '',
              wardId: '',
            }}
          >
            <Grid middle container spacing={2}>
              <MonthInput
                date
                source={'time'}
                label={this.props.translate('generic.typeTime.month')}
                style={{ marginLeft: '5px', width: '130px' }}
              />
              <ReferenceInput
                reference="geoprovinces"
                source="provinceId"
                allowEmpty
                onChange={this.onChangeProvince}
                style={{ marginLeft: '20px', width: '100px' }}
              >
                <SelectInput optionText="name" />
              </ReferenceInput>
              <ReferenceInput
                reference="geodistricts"
                source="districtId"
                key={this.state.provinceId || 'district'}
                filter={{ provinceId: this.state.provinceId || '' }}
                allowEmpty
                onChange={this.onChangeDistrict}
                style={{ marginLeft: '20px', width: '100px' }}
              >
                <SelectInput optionText="name" />
              </ReferenceInput>
              <ReferenceInput
                reference="geowards"
                source="wardId"
                key={this.state.districtId || 'ward'}
                filter={{ districtId: this.state.districtId || '' }}
                allowEmpty
                style={{ marginLeft: '20px', width: '170px' }}
              >
                <SelectInput optionText="name" />
              </ReferenceInput>
            </Grid>
          </FlexFormFilter>
        </Grid>
      </Grid>
    );
  }
}
const mapStateToProps = state => {
  return {
    geodistricts: state.admin.resources.geodistricts,
    geowards: state.admin.resources.geowards,
    geoquarters: state.admin.resources.geoquarters,
    geoprovinces: state.admin.resources.geoprovinces,
  };
};
FilterTimeGeo.defaultProps = {
  formName: 'formFilterTimeGeo',
};
FilterTimeGeo.propTypes = {
  title: PropTypes.string,
  classes: PropTypes.object,
  dataProvider: PropTypes.func,
  translate: PropTypes.func,
  dispatch: PropTypes.any,
  geodistricts: PropTypes.object,
  geowards: PropTypes.object,
  geoquarters: PropTypes.object,
  geoprovinces: PropTypes.object,
  getFilter: PropTypes.func,
  formName: PropTypes.string,
};
const enhance = compose(translate, withStyles(styles), withDataProvider, connect(mapStateToProps));
export default enhance(FilterTimeGeo);
