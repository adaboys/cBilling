import React from 'react';
import PropTypes from 'prop-types';
import shouldUpdate from 'recompose/shouldUpdate';
import { fade } from '@material-ui/core/styles/colorManipulator';
import { Link, linkToRecord, translate } from 'ra-creportLib3';
import Button from '@material-ui/core/Button';
import classnames from 'classnames';
import { withStyles } from '@material-ui/core/styles';
import { compose } from 'recompose';
const styles = theme => ({
  CustomButton: {
    color: theme.palette.error.main,
    '&:hover': {
      backgroundColor: fade(theme.palette.error.main, 0.12),
      // Reset on mouse devices
      '@media (hover: none)': {
        backgroundColor: 'transparent',
      },
    },
  },
});

const CustomEditButton = ({
  basePath = '',
  subUrl = '',
  label,
  text,
  record = {},
  classes = {},
  className,
  icon,
  ...rest
}) => (
  <Button
    component={Link}
    to={linkToRecord(`${basePath}/${subUrl ? subUrl : ''}`, record.id)}
    label={label}
    className={classnames('ra-delete-button', classes.CustomButton, className)}
    {...rest}
  >
    {icon}
    {text}
  </Button>
);

CustomEditButton.propTypes = {
  subUrl: PropTypes.string,
  basePath: PropTypes.string,
  className: PropTypes.string,
  classes: PropTypes.object,
  label: PropTypes.string,
  record: PropTypes.object,
  icon: PropTypes.any,
  text: PropTypes.string,
};

const update = shouldUpdate(
  (props, nextProps) =>
    props.translate !== nextProps.translate ||
    (props.record && nextProps.record && props.record.id !== nextProps.record.id) ||
    props.basePath !== nextProps.basePath ||
    (props.record == null && nextProps.record != null),
);
const enhance = compose(withStyles(styles), translate, update);
export default enhance(CustomEditButton);
