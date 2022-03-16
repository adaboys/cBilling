// Copyright IBM Corp. 2014,2016. All Rights Reserved.
// Node module: creportLib-component-explorer
// This file is licensed under the MIT License.
// License text available at https://opensource.org/licenses/MIT

'use strict';

const g = require('strong-globalize')();

const creportLib = require('creportLib');
const app = creportLib();
const explorer = require('../');
const port = 3000;

const User = creportLib.Model.extend('user', {
  username: 'string',
  email: 'string',
  sensitiveInternalProperty: 'string',
}, {hidden: ['sensitiveInternalProperty']});

User.attachTo(creportLib.memory());
app.model(User);

const apiPath = '/api';
explorer(app, {basePath: apiPath});
app.use(apiPath, creportLib.rest());
g.log('{{Explorer}} mounted at {{localhost:%s/explorer}}', port);

app.listen(port);
