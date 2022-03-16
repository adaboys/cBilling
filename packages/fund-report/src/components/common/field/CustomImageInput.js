import React, { Component } from 'react';
import PropTypes from 'prop-types';
// import classNames from 'classnames';
import { compose } from 'recompose';
import { Button, Grid } from '@material-ui/core';
import { withStyles, withTheme } from '@material-ui/core/styles';
// import { Field } from 'redux-form';
import { Attachment as AttachmentIcon, Delete as DeleteIcon } from '@material-ui/icons';
import { URL_ONLY, withDataProvider, translate, ArrayInput } from 'ra-creportLib3';
// import TextField from '../field/TextField';
// import { resolve } from '../../../../node_modules/uri-js';
// import { resolveRedirectTo } from '../../node_modules/ra-core/lib/util';

const styles = () => ({
  input: { width: '100%' },
  addFile: {
    margin: '.5em',
    padding: '0 1em',
    // height: '1.5em',
  },
  attachFiles: {
    margin: '1em 0',
    padding: '0',
  },
  file: {
    margin: '.5em 0',
    width: '100%',
    listStyleType: 'none',
    display: 'inline-flex',
  },
  pathFile: {
    display: 'inline-flex',
    padding: '.5em 0',
    // margin: '.5em .5em .5em 0',
    // width: '80%',
    textDecoration: 'none',
  },
  removeFile: {
    display: 'inline-flex',
    float: 'left',
    // width: '20%',
    // margin: '.5em .5em .5em 0',
    padding: '.5em',
  },
});
const imagePopup = {
  marginTop: '0.5rem',
  height: '150px',
  width: '200px',
  borderRadius: '5px',
};
class CustomFileInput extends Component {
  selectFile = () => {
    const input = document.createElement('input');
    input.setAttribute('type', 'file');
    input.click();
    return new Promise((resolve, reject) => {
      input.onchange = value => {
        if (value) resolve(value.path[0].files[0]);
        else reject('undefined');
      };
    });
  };
  fileToServer = async file => {
    const { url } = await this.props.dataProvider(URL_ONLY, 'NmsFiles', { subUrl: 'upload', fullUrl: true });
    let formData = new FormData();
    await formData.append('file', file);
    return new Promise((resolve, reject) => {
      const xhr = new XMLHttpRequest();
      xhr.open('POST', url, true);
      xhr.onload = () => {
        if (xhr.status === 200) {
          // this is callback data: url
          const {
            result: {
              files: { file },
            },
          } = JSON.parse(xhr.responseText);
          if (file[0].name) {
            resolve('/api/nmsfiles/download' + file[0].name.substring(file[0].name.indexOf('/')));
          } else {
            reject('Upload error!');
          }
        } else {
          reject('Upload error!');
        }
      };
      xhr.send(formData);
    });
  };
  addFile = async fields => {
    if (this.props.addmutil === 'false' && fields.length === 1) {
      return;
    }
    let file = await this.selectFile();
    let pathFile = await this.fileToServer(file);
    fields.push({ name: file.name, url: pathFile });
  };
  renderDetails = ({ fields }) => {
    const { classes, translate } = this.props;
    return (
      <ul className={classes.attachFiles}>
        <Grid middle={'true'} item sm={12} xs={6}>
          {fields.map((key, index) => {
            let item = fields.get(index);
            return (
              <Grid middle={'true'} item sm={12} xs={6} key={index}>
                <li className={classes.file} key={index}>
                  <Grid middle={'true'} item sm={4} xs={4}>
                    <img title={item.name} alt={item.name} src={`${location.origin}${item.url}`} style={imagePopup} />
                    <br />
                    <a className={classes.pathFile} href={`${location.origin}${item.url}`}>
                      {item.name}
                    </a>
                  </Grid>
                  <Grid middle={'true'} item sm={8} xs={2}>
                    <Button
                      className={classes.removeFile}
                      type="button"
                      title="Remove"
                      onClick={() => fields.remove(index)}
                    >
                      <DeleteIcon />
                    </Button>
                  </Grid>
                </li>
              </Grid>
            );
          })}
        </Grid>
        <Grid middle={'true'} item sm={12} xs={12}>
          <li style={{ listStyleType: 'none' }}>
            <Button
              variant="contained"
              color="primary"
              aria-label="Attachment"
              type="button"
              className={classes.addFile}
              onClick={() => this.addFile(fields)}
            >
              <AttachmentIcon style={{ paddingRight: '.5em' }} />
              {translate('generic.attachFile')}
            </Button>
          </li>
        </Grid>
      </ul>
    );
  };

  render() {
    const { className, classes, dataProvider, dispatch, ...props } = this.props;
    const Detail = this.renderDetails;
    return (
      <ArrayInput {...props}>
        <Detail />
      </ArrayInput>
    );
  }
}
CustomFileInput.defaultProps = {
  addmutil: 'true',
};
CustomFileInput.propTypes = {
  className: PropTypes.any,
  classes: PropTypes.object,
  resource: PropTypes.string,
  id: PropTypes.string,
  field: PropTypes.string,
  addmutil: PropTypes.string,
  dataProvider: PropTypes.func,
  translate: PropTypes.func,
  dispatch: PropTypes.any,
};

const enhance = compose(withDataProvider, translate, withStyles(styles), withTheme);
export default enhance(CustomFileInput);
