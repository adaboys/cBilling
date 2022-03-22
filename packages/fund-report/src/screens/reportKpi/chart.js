import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { Create, FlexForm, TextInput, required, translate, EditorInput, DateTimeInput } from 'ra-creportLib3';
import { Grid } from '@material-ui/core';
import compose from 'recompose/compose';
import { PasswordInput } from 'react-admin';
import { Chart } from 'react-google-charts';
import {
 
  Legend,
  
  BarChart,
  CartesianGrid,
  XAxis,
  YAxis,
  Tooltip,
  Bar,
} from 'recharts';


let data;

const options = {
  backgroundColor: 'transparent',
  title: 'Project statistic',
};

const data3 = [];

const data4 = [];

const data5 = [];

const options5 = {
  backgroundColor: 'transparent',
  title: 'Summarized cBilling',
};

class CreatePostJob extends Component {
  render() {
    const { props } = this;

    return (
      <div>
        <Grid middle container spacing={4} direction="row" justifyContent="center" alignItems="center">
          <Grid middle item xs={12} sm={6}>
            <div>cBilling statistic (1k Ada)</div>
            <br />
            <BarChart width={730} height={280} data={data3}>
              <CartesianGrid strokeDasharray="3 3" />
              <XAxis dataKey="name" />
              <YAxis />
              <Tooltip />
              <Legend style={{ fontSize: 10 }} />
              <Bar dataKey="Requested budget(1k Ada)" fill="#8884d8" />
              <Bar dataKey="Transferred amount (1k Ada)" fill="#82ca9d" />
              <Bar dataKey="Remained amount (1k Ada)" fill="#d88884" />
            </BarChart>
          </Grid>

          <Grid middle item xs={12} sm={6}>
            <div>Project statistic</div>
            <br />
            <BarChart width={730} height={280} data={data4}>
              <CartesianGrid strokeDasharray="3 3" />
              <XAxis dataKey="name" />
              <YAxis />
              <Tooltip />
              <Legend style={{ fontSize: 10 }} />
              <Bar dataKey="Approved projects" fill="#8884d8" />
              <Bar dataKey="Completed projects" fill="#82ca9d" />
              <Bar dataKey="Pending projects" fill="#8884d8" />
            </BarChart>{' '}
          </Grid>

          <Grid right item xs={12} sm={6}>
            <Chart chartType="PieChart" data={data5} options={options5} width={'100%'} height={'300px'} />
          </Grid>
          <Grid middle item xs={12} sm={6}>
            <Chart chartType="PieChart" data={data} options={options} width={'100%'} height={'300px'} />
          </Grid>
        </Grid>
      </div>
    );
  }
}

CreatePostJob.propTypes = {
  translate: PropTypes.func,
  hasList: PropTypes.bool,
  hasShow: PropTypes.bool,
  hasCreate: PropTypes.bool,
  hasEdit: PropTypes.bool,
  staticcontext: PropTypes.any,
};
CreatePostJob.detaultProps = {
  hasList: true,
  hasShow: true,
  hasCreate: false,
  hasEdit: false,
};

const enhance = compose(translate);
export default enhance(CreatePostJob);
