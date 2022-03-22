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
      <List {...rest} filters={<Filters />} resource="reportcBillings"  title={walletAddress} hasCreate={true}  bulkActionButtons={false}>
        <Datagrid >
          <TextField source="name" label="Transaction name" />
         
          <TextField source="hash" label="Hash"/>
          <TextField source="type" label="Type"/>
          <NumberField source="value" label="Amount (Ada)"/>
          <DateField source="createdDate" label="TransactionType date" />
        

       
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
