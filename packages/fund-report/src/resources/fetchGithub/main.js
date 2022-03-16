import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { translate } from 'ra-creportLib3';
import { Grid } from '@material-ui/core';
import compose from 'recompose/compose';

import {
  RadarChart,
  PolarGrid,
  PolarRadiusAxis,
  PolarAngleAxis,
  Radar,
  Legend,
  Treemap,
  BarChart,
  CartesianGrid,
  XAxis,
  YAxis,
  Tooltip,
  Bar,
} from 'recharts';

class CreatePostJob extends Component {
  render() {
    const { props } = this;

    return (
      <div>
        <Grid middle container spacing={2} direction="row" justifyContent="center" alignItems="center">
          <Grid middle item xs={12} sm={6}>
            <BarChart width={730} height={250} data={barchart1}>
              <CartesianGrid strokeDasharray="3 3" />
              <XAxis dataKey="name" />
              <YAxis />
              <Tooltip />
              <Legend />
              <Bar dataKey="job posts" fill="#8884d8" />
              <Bar dataKey="job bids" fill="#82ca9d" />
              <Bar dataKey="contracted jobs" fill="#82ca9d" />
              <Bar dataKey="completed contracts" fill="#82ca9d" />
            </BarChart>
          </Grid>

          <Grid middle item xs={12} sm={6}>
            <BarChart width={730} height={250} data={barchart2}>
              <CartesianGrid strokeDasharray="3 3" />
              <XAxis dataKey="name" />
              <YAxis />
              <Tooltip />
              <Legend />
              <Bar dataKey="total employers" fill="#8884d8" />
              <Bar dataKey="active employers" fill="#82ca9d" />
              <Bar dataKey="total job seekers" fill="#8884d8" />
              <Bar dataKey="active job seekers" fill="#82ca9d" />
            </BarChart>{' '}
          </Grid>
          <Grid middle item xs={12} sm={6}>
            <RadarChart outerRadius={90} width={730} height={250} data={radadata}>
              <PolarGrid />
              <PolarAngleAxis dataKey="subject" />
              <PolarRadiusAxis angle={30} domain={[0, 150]} />
              <Radar name="Employer" dataKey="A" stroke="#8884d8" fill="#8884d8" fillOpacity={0.6} />
              <Radar name="Job seeker" dataKey="B" stroke="#82ca9d" fill="#82ca9d" fillOpacity={0.6} />
              <Legend />
            </RadarChart>
          </Grid>
          <Grid right item xs={12} sm={6}>
            <div style={{ textAlign: 'right', color: 'red', marginLeft: '60px' }}>
              <Treemap
                style={{ textAlign: 'right', color: 'red' }}
                width={660}
                fullWidth
                height={220}
                data={treemapData}
                dataKey="size"
                ratio={4 / 3}
                stroke="#fff"
                fill="#8884d8"
              />
            </div>
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
