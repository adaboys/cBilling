//tham khảo: https://www.epa.ie/pubs/advice/creport/quality/creport_Quality.pdf
import React, { Component } from 'react';
import PropTypes from 'prop-types';
import {
  translate,
  TextField,
  List,
  Datagrid,
  EditButton,
  ShowButton,
  SelectField,
  Filter,
  TextInput,
  DateField,
  NumberField,
} from 'ra-creportLib3';
import { compose } from 'recompose';
import config from '../../Config';

const Filters = props => (
  <Filter {...props}>
    <TextInput source="name" label={'generic.search'} alwaysOn />
  </Filter>
);
let walletAddress = "My wallet address: addr_test1qq0rug5wxyd2fnwdyvlc5vc6jk8tq59nwjn7xx5gdwdj9p43ge73vmf7xvkn23tkyq30gd2jtlgztf3rw0mtvkjzv4vqr6wnz3"
class ListPostJob extends Component {
  render() {
    const { translate, ...rest } = this.props;
   
    return (
      <List {...rest} filters={<Filters />} resource="withdraws"  title={walletAddress} hasCreate={true}>
        <Datagrid>
          <TextField source="name" label="Name"/>
         
          <TextField source="hash" label="Hash"/>
          <NumberField source="value" label="Amount (ADA)"/>
          <DateField source="createdDate" label="Created date" />
        

       
          <ShowButton label="View Detail" />
          <EditButton />
        </Datagrid>
      </List>
    );
  }
}

ListPostJob.propTypes = {
  translate: PropTypes.func,
  hasList: PropTypes.bool,
  hasShow: PropTypes.bool,
  hasCreate: PropTypes.bool,
  hasEdit: PropTypes.bool,
};

export default compose(translate)(ListPostJob);
