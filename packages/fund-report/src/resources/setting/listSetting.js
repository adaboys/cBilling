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
  ArrayField,
  SingleFieldList,
  ChipField
} from 'ra-creportLib3';
import {Chip} from "@material-ui/core"
import { compose } from 'recompose';
import config from '../../Config';

const Filters = props => (
  <Filter {...props}>
    <TextInput source="name" label={'generic.search'} alwaysOn />
  </Filter>
);



const TagsField = ({ record }) => (
  record.skills? 
  <ul>
      {record.skills.map(item => (
          <Chip key={item} label={item} />
      ))}
  </ul>
  : null
)
TagsField.defaultProps = {
  addLabel: true
};

class ListPostJob extends Component {
  render() {
    const { translate, ...rest } = this.props;
    return (
      <List {...rest} filters={<Filters />} resource="settings">
        <Datagrid>
          <TextField source="name" />
          <TextField source="value" />
         
          <DateField source="createdDate" label="Created date" />
          
          <ShowButton label="View Detail"/>
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
