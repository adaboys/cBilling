import React, { Component, Fragment } from 'react';
import { connect } from 'react-redux';
import {
  FlexFormFilter,
  translate,
  SelectInput,
  withDataProvider,
  CUSTOM,
  Button,
  TimeRangeInput,
  SelectArrayInput,
} from 'ra-creportLib3';
import { withStyles, withTheme } from '@material-ui/core/styles';
import { compose } from 'recompose';
import PropTypes from 'prop-types';
import { Grid, Paper } from '@material-ui/core';
import { StatisticButtonIcon, PrintIcon } from '../../../styles/Icons';
import config from '../../../Config';
import get from 'lodash/get';
import { change } from 'redux-form';

const styles = theme => {
  return {
    paper: {
      marginTop: theme.spacing(1),
      marginBottom: theme.spacing(1),
    },
    widthFormGroup: { float: 'left', paddingRight: '8px' },
  };
};

class FilterReportcreportSource extends Component {
  formRefFilter = React.createRef();
  constructor(props) {
    super(props);
    this.selectAll = config.selectAll;
    this.conditionTypes = config.filterConditions.map(({ conditions, ...rest }) => rest);
    this.filterConditions = config.filterConditions;

    this.state = {
      allcreportSourceGroups: [],
      allcreportSources: [],
      allcreportParams: [],

      creportSourceChoices: [],
      selectedcreportSources: [],
      selectedSourcecreportGroup: null,
      selectedcreportSource: null,

      filter: {},
    };
  }
  componentDidMount() {
    const { showcreportParameter, flgMaterial } = this.props;

    this.getAllcreportGroups();
    this.getAllcreportSources();

    if (showcreportParameter) {
      this.getcreportParams();
    }

    if (flgMaterial) {
      let conditionChoices = this.filterConditions.filter(item => item.id == '1')[0].conditions;
      this.setState({ conditionChoices: conditionChoices, selectedConditionType: '1' }, () => this.submitFilter());
    }
  }

  getAllcreportGroups = () => {
    let { dataProvider } = this.props;
    dataProvider(CUSTOM, 'creportsourcegroups', { filter: { fields: { id: true, name: true } } }).then(res => {
      this.setState({ allcreportSourceGroups: [...res.data, ...this.selectAll] });
    });
  };

  getAllcreportSources = () => {
    let { dataProvider, showcreportParameter } = this.props;
    dataProvider(CUSTOM, 'creportsources', {
      filter: { fields: { id: true, name: true, creportSourceGroupId: true } },
    }).then(res => {
      if (showcreportParameter) {
        this.setState({
          creportSourceChoices: [...res.data, ...this.selectAll],
          allcreportSources: [...res.data, ...this.selectAll],
          selectedcreportSources: [...res.data, ...this.selectAll],
        });
      } else {
        this.setState(
          {
            creportSourceChoices: [...res.data, ...this.selectAll],
            allcreportSources: [...res.data, ...this.selectAll],
            selectedcreportSources: [...res.data, ...this.selectAll],
          },
          () => this.submitFilter(),
        );
      }
    });
  };

  onChangeSourceGroup = (e, val) => {
    let { formName, change } = this.props;
    let { allcreportSources } = this.state;
    this.setState({ selectedSourcecreportGroup: val });

    //select all
    if (val === this.selectAll[0].id) {
      this.setState({ creportSourceChoices: allcreportSources });
    } else {
      this.setState({
        creportSourceChoices: allcreportSources.filter(item => item.creportSourceGroupId === val || item.id === 'all'),
        selectedcreportSources: allcreportSources.filter(item => item.creportSourceGroupId === val || item.id === 'all'),
      });
    }
    change(formName, 'creportSource', 'all');
  };

  onChangecreportSource = (e, val) => {
    let { creportSourceChoices } = this.state;
    if (val !== this.selectAll[0].id) {
      let selectedcreportSources = [];
      selectedcreportSources.push({ id: val });
      this.setState({ selectedcreportSources: selectedcreportSources });
    } else {
      let selectedcreportSources = creportSourceChoices.map(({ creportSourceGroupId, name, ...rest }) => rest);
      this.setState({ selectedcreportSources: selectedcreportSources });
    }
  };

  onChangeTime = dataTime => {
    this.setState({
      typeTime: dataTime.typeTime,
      valueTimeFrom: dataTime.valueTimeFrom,
      valueTimeTo: dataTime.valueTimeTo,
    });
  };

  onChangecreportParameter = (e, val) => {
    this.setState({ selectedcreportParam: { id: val } });
  };

  getcreportParams = () => {
    let { formName, change, dataProvider } = this.props;
    dataProvider(CUSTOM, 'creportparameters', { filter: { fields: { id: true, name: true, symbol: true } } }).then(
      res => {
        change(formName, 'creportParam', res.data[0].id);
        this.setState(
          {
            allcreportParams: res.data,
            selectedcreportParam: res.data[0].id,
            selectedParamSymbol: res.data[0].symbol.toLowerCase(),
          },
          () => this.submitFilter(),
        );
      },
    );
  };

  onChangecreportParameter = (e, val) => {
    let selectedParamSymbol = this.state.allcreportParams.filter(item => item.id == val)[0].symbol.toLowerCase();
    this.setState({ selectedcreportParam: val, selectedParamSymbol: selectedParamSymbol });
  };

  onChangeSelectType = (e, val) => {
    const { change, formName } = this.props;
    let conditionChoices = this.filterConditions.filter(item => item.id == val)[0].conditions;

    change(
      formName,
      'selectCondition',
      conditionChoices.map(item => item.id),
    ); // set default

    this.setState({ conditionChoices: conditionChoices, selectedConditionType: val });
  };
  onChangeSelectMaterial = (e, val) => {
    const { change, formName } = this.props;
    //let conditionChoices = this.filterConditions.filter(item => item.id == val)[0].conditions;

    change(formName, 'selectMaterial', val); // set default

    // this.setState({ conditionChoices: conditionChoices, selectedConditionType: val });
  };
  onChangeCondition = (e, val) => {
    this.setState({ selectedConditions: val });
  };

  submitFilter = () => {
    let { selectedcreportSources, selectedParamSymbol, selectedcreportParam } = this.state;
    let { showcreportParameter, flgMaterial, flgDetail, flgChart, queryReport } = this.props;

    let filter = {};

    let tmp = get(this.formRefFilter, 'current.props.values');
    // console.log('before format filter: ', tmp);

    // filter of chart
    if (flgChart) {
      // console.log('after format filter chart: ', tmp);
      tmp.selectedParamSymbol = selectedParamSymbol;
      queryReport(tmp);
    }

    let arr = [];
    for (let i = 0; i < selectedcreportSources.length; i++) {
      let item = selectedcreportSources[i];
      if (item.id !== this.selectAll[0].id) arr.push({ id: item.id });
    }
    if (!arr.length) return;

    // filter of list
    if (flgDetail) {
      filter.selectedcreportSources = arr;
      filter.typeTime = get(tmp, 'timeRange.type');
      filter.valueTimeFrom = get(tmp, 'timeRange.from');
      filter.valueTimeTo = get(tmp, 'timeRange.to');
      if (showcreportParameter) {
        filter.selectedcreportParam = selectedcreportParam; // id
        filter.selectedParamSymbol = selectedParamSymbol; // name
      }
      // console.log('after format filter detail: ', filter);
      queryReport(filter);
    }

    // filter of report material
    if (flgMaterial) {
      filter.selectedcreportSources = arr;
      filter.conditionType = get(tmp, 'selectType');
      filter.selectConditions = get(tmp, 'selectCondition');
      filter.selectMaterial = get(tmp, 'selectMaterial');
      // console.log('after format filter material: ', filter);
      queryReport(filter);
    }
    // this.setState({ filter: filter });
  };
  render() {
    const {
      showcreportParameter,
      translate,
      handlePrint,
      classes,
      typeTimes,
      formName,
      hasPrint,
      defaultFilter,
      flgMaterial,
    } = this.props;

    let { allcreportSourceGroups, creportSourceChoices, allcreportParams, filter, conditionChoices } = this.state;

    return (
      <Paper>
        <FlexFormFilter formRef={this.formRefFilter} formName={formName} defaultValue={defaultFilter}>
          <Grid middle container>
            <SelectInput
              source="sourceGroup"
              label={translate('resources.reportmaterials.fields.selectGroup')}
              choices={allcreportSourceGroups}
              style={{ marginLeft: '5px', marginTop: '25px' }}
              onChange={this.onChangeSourceGroup}
            />
            <SelectInput
              source="creportSource"
              label={translate('resources.reportmaterials.fields.selectSource')}
              choices={creportSourceChoices}
              style={{ marginLeft: '5px', marginTop: '25px' }}
              onChange={this.onChangecreportSource}
            />
            {showcreportParameter && (
              <SelectInput
                source="creportParam"
                label={translate('resources.reportqualities.fields.selectParameter')}
                choices={allcreportParams}
                style={{ marginLeft: '5px', marginTop: '25px' }}
                defaultValue={'flow'}
                onChange={this.onChangecreportParameter}
              />
            )}
            {flgMaterial && (
              <Fragment>
                <SelectInput
                  source="selectMaterial"
                  label={translate('resources.reportmaterials.fields.selectMaterial')}
                  choices={config.selectMaterial}
                  style={{ marginLeft: '5px' }}
                  onChange={this.onChangeSelectMaterial}
                />

                <SelectInput
                  source="selectType"
                  label={translate('resources.reportmaterials.fields.selectType')}
                  choices={this.conditionTypes}
                  style={{ marginLeft: '5px' }}
                  onChange={this.onChangeSelectType}
                />

                <SelectArrayInput
                  label={translate('resources.reportmaterials.fields.selectCondition')}
                  choices={conditionChoices}
                  source="selectCondition"
                  style={{ marginLeft: '5px', width: '200px' }}
                  onChange={this.onChangeCondition}
                />
              </Fragment>
            )}

            {!flgMaterial && (
              <TimeRangeInput
                style={{ marginLeft: '5px', marginTop: '25px' }}
                fullWidth
                label={''}
                types={typeTimes}
                source={'timeRange'}
                formClassName={classes.widthFormGroup}
              />
            )}
            <Button
              label={translate('generic.statistic.labelButtonStatistic')}
              style={{ marginTop: '35px', marginLeft: '0px', width: '120px', align: 'right' }}
              onClick={this.submitFilter}
            >
              <StatisticButtonIcon />
            </Button>

            {hasPrint && (
              <Button
                label={translate('generic.print')}
                style={{ marginTop: '35px', marginLeft: '0px', width: '50px', align: 'right', float: 'left' }}
                onClick={handlePrint}
                disabled={Object.keys(filter).length === 0}
              >
                <PrintIcon />
              </Button>
            )}
          </Grid>
        </FlexFormFilter>
      </Paper>
    );
  }
}

FilterReportcreportSource.propTypes = {
  translate: PropTypes.func,
  classes: PropTypes.object,
  queryReport: PropTypes.func,
  dataProvider: PropTypes.any,
  handlePrint: PropTypes.func,
  change: PropTypes.func,
  formName: PropTypes.string.isRequired,
  typeTimes: PropTypes.array,
  hasPrint: PropTypes.bool,
  formRef: PropTypes.object,

  defaultFilter: PropTypes.object.isRequired,

  showcreportParameter: PropTypes.bool, // show parameter creport

  flgMaterial: PropTypes.bool, // filter of report material
  flgChart: PropTypes.bool, // filter of chart
  flgDetail: PropTypes.bool, // filter of list
};
FilterReportcreportSource.defaultProps = {
  hasList: true,
  hasShow: true,
  hasCreate: false,
  hasEdit: false,
  hasPrint: false,
  showcreportParameter: false,

  flgDetail: false,
  flgMaterial: false,
  flgChart: false,
};
const enhance = compose(connect(null, { change }), withTheme, withStyles(styles), translate, withDataProvider);

export default enhance(FilterReportcreportSource);
