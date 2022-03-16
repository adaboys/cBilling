// Copyright IBM Corp. 2013,2016. All Rights Reserved.
// Node module: creportLib-component-explorer
// This file is licensed under the MIT License.
// License text available at https://opensource.org/licenses/MIT

'use strict';

const g = require('strong-globalize')();

const creportLib = require('creportLib');
const app = creportLib();
const explorer = require('../');
const port = 3000;

const Product = creportLib.PersistedModel.extend('product', {
  foo: {type: 'string', required: true},
  bar: 'string',
  aNum: {type: 'number', min: 1, max: 10, required: true, default: 5},
});
Product.attachTo(creportLib.memory());
app.model(Product);

const apiPath = '/api';
explorer(app, {basePath: apiPath});
app.use(apiPath, creportLib.rest());
g.log('{{Explorer}} mounted at {{http://localhost:%s/explorer}}', port);

app.listen(port);
