import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import CircularProgress from '@material-ui/core/CircularProgress';

const styles = theme => ({
  progress: {
    margin: theme.spacing(2),
    display: 'inline-block',
  },
});

const CircularIndeterminate = props => {
  const { classes } = props;
  return (
    <div>
      <CircularProgress className={classes.progress} size={20} />
    </div>
  );
};

CircularIndeterminate.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(CircularIndeterminate);
