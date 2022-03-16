import React from 'react';
import PropTypes from 'prop-types';
import _ from 'lodash';
import pure from 'recompose/pure';

import { Clear as FalseIcon, Done as TrueIcon } from '@material-ui/icons';

export const BooleanField = ({ source, record = {}, elStyle }) => {
  if (!record || _.get(record, source) === false || record[source] === undefined) {
    return <FalseIcon style={elStyle} />;
  }

  if (_.get(record, source) === true) {
    return <TrueIcon style={elStyle} />;
  }

  return <span style={elStyle} />;
};

BooleanField.propTypes = {
  addLabel: PropTypes.bool,
  elStyle: PropTypes.object,
  label: PropTypes.string,
  record: PropTypes.object,
  source: PropTypes.string.isRequired,
};

const BooleanFieldCustom = pure(BooleanField);

BooleanFieldCustom.defaultProps = {
  addLabel: true,
  elStyle: {
    display: 'block',
  },
};

export default BooleanFieldCustom;
