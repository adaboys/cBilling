import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { Create, FlexForm, TextInput, required, translate, EditorInput, DateTimeInput } from 'ra-creportLib3';
import compose from 'recompose/compose';
import { PasswordInput } from 'react-admin';
import Select from './select'
import {TextField, Button, Grid} from '@material-ui/core'
//January, February, March, April, May, June, July, August, September, October, November, December
import Table from './table'
import { add } from 'lodash';

let address = "addr_test1qq0rug5wxyd2fnwdyvlc5vc6jk8tq59nwjn7xx5gdwdj9p43ge73vmf7xvkn23tkyq30gd2jtlgztf3rw0mtvkjzv4vqr6wnz3"

class CreatePostJob extends Component {
  render() {
    const { props } = this;

    return (
     
        <Grid middle container spacing={2} direction="row" >
          <Grid middle item xs={12} sm={2}  >
          <Select></Select>
          </Grid>

          <Grid middle item xs={12} sm={10} >
          <TextField id="standard-basic" label="Value"  style={{marginTop: 60, marginLeft: 0, width: "100%"}} defaultValue={address}/>
          </Grid>
          <Grid middle item xs={12} sm={8}>
          <Button variant="outlined" color="secondary">
  Query
</Button>
          </Grid>
          <Grid right item xs={12} sm={12}>
           
        <Table />
          </Grid>
        </Grid>
    
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
