import React from 'react';
import PropTypes from 'prop-types';
import { compose } from 'recompose';
import { CustomPage, withDataProvider, CUSTOM } from 'ra-creportLib3';
import { Grid, withTheme } from '@material-ui/core';
import Table1 from './table1'
import Chart from './chart.js';
import Table2 from './table2'



class Dashboard extends React.Component {
 

  render() {
   
    return (
      <CustomPage title={'generic.pages.dashboard'}>
        <Grid container spacing={2}>
       
          <Grid item xs={12} md={12} style={{ display: 'flex' }}>
          <Chart />
          </Grid>
          <Grid item xs={12} md={6} style={{ display: 'flex' }}>
         <Table1></Table1>
          </Grid>
          <Grid item xs={12} md={6} style={{ display: 'flex' }}>
         <Table2></Table2>
          </Grid>
        </Grid>
      </CustomPage>
    );
  }
}

Dashboard.propTypes = {
  dataProvider: PropTypes.func.isRequired,
  theme: PropTypes.object,
};

const enhance = compose(withDataProvider, withTheme);

export default enhance(Dashboard);
