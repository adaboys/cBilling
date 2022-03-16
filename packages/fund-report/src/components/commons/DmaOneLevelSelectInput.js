import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { push } from 'react-router-redux';
import { compose } from 'recompose';
import { SelectInput, translate, withDataProvider, CUSTOM } from 'ra-creportLib3';
import { connect } from 'react-redux';

// hien thi cac Dma cua 1 level
class DmaOneLevelSelectInput extends Component {
  state = {
    data: [],
  };
  componentDidMount() {
    let { level, parentdmaid } = this.props;
    this.getData(level, parentdmaid);
  }
  getData = (level, parentDmaId) => {
    let tmp = {};
    if (parentDmaId) {
      tmp.where = { and: [{ level }, { parentDmaId }] };
    } else {
      tmp.where = { level };
    }
    this.props
      .dataProvider(CUSTOM, 'dmas', {
        query: { filter: JSON.stringify(tmp) },
      })
      .then(res => {
        let tmp = [];
        tmp.push({ id: 'AllDma', name: 'generic.allDma' });
        if (res.data && res.data.length) {
          for (let i = 0; i < res.data.length; i++) {
            let item = {};
            item.id = res.data[i].id;
            item.name = res.data[i].name;
            tmp.push(item);
          }
        }
        this.setState({ data: tmp });
      });
  };
  UNSAFE_componentWillReceiveProps(nextProps) {
    let { level, parentdmaid } = nextProps;
    this.getData(level, parentdmaid);
  }
  onChange = (e, val) => {
    this.props.onChange(val);
  };
  render() {
    let { dataProvider, push, basePath, ...rest } = this.props;
    return (
      <SelectInput
        {...rest}
        source={this.props.source}
        label={this.props.translate('generic.dma')}
        choices={this.state.data}
        style={{ marginLeft: '5px' }}
        defaultValue={'AllDma'}
        onChange={this.onChange}
      />
    );
  }
}
DmaOneLevelSelectInput.defaultProps = {};

DmaOneLevelSelectInput.propTypes = {
  translate: PropTypes.func,
  level: PropTypes.number.isRequired,
  onChange: PropTypes.func.isRequired,
  source: PropTypes.string.isRequired,
  dataProvider: PropTypes.func,
  push: PropTypes.func,
  basePath: PropTypes.string,
  parentdmaid: PropTypes.string,
};
const enhance = compose(
  withDataProvider,
  translate,
  connect(null, {
    push,
  }),
);

export default enhance(DmaOneLevelSelectInput);
