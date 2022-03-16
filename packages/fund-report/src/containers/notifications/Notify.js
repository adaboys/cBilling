import React, { Component, Fragment } from 'react';
import PropTypes from 'prop-types';
import MenuItem from '@material-ui/core/MenuItem';
// import ListItemIcon from '@material-ui/core/ListItemIcon';
import Typography from '@material-ui/core/Typography';
// import PriorityHighIcon from '@material-ui/icons/PriorityHigh';
import { compose } from 'recompose';
import { withDataProvider, CUSTOM, translate } from 'ra-creportLib3';
import config from '../../Config';

class Notify extends Component {
  state = { notifyItems: [] };

  componentDidMount() {
    this.loadNotify();
  }

  loadNotify = () => {
    const { dataProvider } = this.props;
    dataProvider(CUSTOM, 'creportSources', {
      subUrl: 'dashboard',
      method: 'get',
      query: { mode: 'notify' },
    }).then(res => {
      if (res) {
        this.setState({ notifyItems: res.data });
      }
    });
  };

  colorMap = colorCode => {
    let color = null;
    switch (colorCode) {
      case 1:
        color = { color: config.color.status.criticalAlert };
        break;
      case 2:
        color = { color: config.color.status.alert };
        break;
      case 3:
        color = { color: config.color.status.alert };
        break;
      case 4:
        color = { color: config.color.status.criticalAlert };
        break;
      default:
        break;
    }
    return color;
  };

  renderItem = arr =>
    arr.map(item => {
      return (
        <Fragment key={item._id}>
          <MenuItem onClick={this.props.handleClose}>
            <Typography variant="subheading"> {item.dataLoggerName} </Typography>
          </MenuItem>
          {item.alertRecord.totalAlert.map(subItem => {
            return (
              <MenuItem key={subItem.param} style={{ marginLeft: '10px' }} onClick={this.props.handleClose}>
                <Typography gutterBottom noWrap style={this.colorMap(subItem.alert)}>
                  {this.props.translate(`generic.alertLevel.${subItem.param}`)}:{' '}
                  {this.props.translate(`generic.alertLevel.${subItem.alert}`)}
                </Typography>
              </MenuItem>
            );
          })}
        </Fragment>
      );
    });

  render() {
    // const { handleClose } = this.props;
    const { notifyItems } = this.state;
    if (notifyItems.length < 1) {
      return null;
    }

    return <Fragment> {this.renderItem(notifyItems)} </Fragment>;
  }
}

Notify.propTypes = {
  dataProvider: PropTypes.func.isRequired,
  handleClose: PropTypes.func,
  translate: PropTypes.func,
};

const enhance = compose(withDataProvider, translate);

export default enhance(Notify);
