// Copyright IBM Corp. 2013,2016. All Rights Reserved.
// Node module: creportLib-component-explorer
// This file is licensed under the MIT License.
// License text available at https://opensource.org/licenses/MIT

var SG = require('strong-globalize');
var map = require('lodash/map');
var pluralize = require('pluralize');
// var filter = require('lodash/filter');
SG.SetRootDir(__dirname);
var g = SG();

/*!
 * Adds dynamically-updated docs as /explorer
 */
var url = require('url');
var path = require('path');
var urlJoin = require('./lib/url-join');
var _defaults = require('lodash').defaults;
var createSwaggerObject = require('creportLib-swagger').generateSwaggerSpec;
var SWAGGER_UI_ROOT = require('swagger-ui/index').dist;
var STATIC_ROOT = path.join(__dirname, 'public');

module.exports = explorer;
explorer.routes = routes;

/**
 * Example usage:
 *
 * var explorer = require('creportLib-component-explorer');
 * explorer(app, options);
 */

function explorer(creportLibApplication, options) {
  options = _defaults({}, options, { mountPath: '/explorer' });
  map(creportLibApplication.models, (model, key) => {
    creportLibApplication.use(options.mountPath + '/' + key, routes(creportLibApplication, options, key));
  });
  // OrgConfig
  // let key = 'OrgConfig'
  // creportLibApplication.use(
  //   options.mountPath + '/' + key,
  //   routes(creportLibApplication, options, key)
  // );
  creportLibApplication.set('creportLib-component-explorer', options);
}

function routes(creportLibApplication, options, model) {
  var creportLib = creportLibApplication.creportLib;
  var creportLibMajor = (creportLib && creportLib.version && creportLib.version.split('.')[0]) || 1;

  if (creportLibMajor < 2) {
    throw new Error(g.f('{{creportLib-component-explorer}} requires ' + '{{creportLib}} 2.0 or newer'));
  }

  options = _defaults({}, options, {
    resourcePath: 'swagger.json',
    apiInfo: creportLibApplication.get('apiInfo') || {},
    swaggerUI: true,
  });

  var router = new creportLib.Router();

  mountSwagger(creportLibApplication, router, options, model);

  // config.json is loaded by swagger-ui. The server should respond
  // with the relative URI of the resource doc.
  router.get('/config.json', function(req, res) {
    // Get the path we're mounted at. It's best to get this from the referer
    // in case we're proxied at a deep path.
    var source = url.parse(req.headers.referer || '').pathname;
    // strip index.html if present in referer
    if (source && /\/index\.html$/.test(source)) {
      source = source.replace(/\/index\.html$/, '');
    }
    // If no referer is available, use the incoming url.
    if (!source) {
      source = req.originalUrl.replace(/\/config.json(\?.*)?$/, '');
    }
    res.send({
      url: urlJoin(source, '/' + options.resourcePath),
      auth: options.auth,
    });
  });

  if (options.swaggerUI) {
    // Allow specifying a static file roots for swagger files. Any files in
    // these folders will override those in the swagger-ui distribution.
    // In this way one could e.g. make changes to index.html without having
    // to worry about constantly pulling in JS updates.
    if (options.uiDirs) {
      if (typeof options.uiDirs === 'string') {
        router.use(creportLib.static(options.uiDirs));
      } else if (Array.isArray(options.uiDirs)) {
        options.uiDirs.forEach(function(dir) {
          router.use(creportLib.static(dir));
        });
      }
    }

    // File in node_modules are overridden by a few customizations
    router.use(creportLib.static(STATIC_ROOT));

    // Swagger UI distribution
    router.use(creportLib.static(SWAGGER_UI_ROOT));
  }

  return router;
}

/**
 * Setup Swagger documentation on the given express app.
 *
 * @param {Application} creportLibApplication The creportLib application to
 * document.
 * @param {Application} swaggerApp Swagger application used for hosting
 * swagger documentation.
 * @param {Object} opts Options.
 */
var swaggerObject = null;
function mountSwagger(creportLibApplication, swaggerApp, opts, model) {
  function rebuildSwaggerObject() {
    swaggerObject = createSwaggerObject(creportLibApplication, opts);
  }
  if (!swaggerObject) {
    swaggerObject = createSwaggerObject(creportLibApplication, opts);
    // listening to modelRemoted event for updating the swaggerObject
    // with the newly created model to appear in the Swagger UI.
    creportLibApplication.on('modelRemoted', rebuildSwaggerObject);

    creportLibApplication.on('modelDeleted', rebuildSwaggerObject);

    // listening to started event for updating the swaggerObject
    // when a call to app.models.[modelName].nestRemoting([modelName])
    // to appear that method in the Swagger UI.
    creportLibApplication.on('remoteMethodAdded', rebuildSwaggerObject);

    // listening to remoteMethodDisabled event for updating the swaggerObject
    // when a remote method is disabled to hide that method in the Swagger UI.
    creportLibApplication.on('remoteMethodDisabled', rebuildSwaggerObject);
  }
  var resourcePath = (opts && opts.resourcePath) || 'swagger.json';
  // resourcePath = `/${model}${resourcePath}`
  if (resourcePath[0] !== '/') resourcePath = '/' + resourcePath;
  // console.log(resourcePath);
  // const definitions = filter(swaggerObject.definitions, (item, key)=> key.indexOf(model) >=0);
  // const paths = filter(swaggerObject.paths, (item, key)=>key.indexOf(model)>=0);
  // const tags = filter(swaggerObject.tags, (item, key)=>key.indexOf(model)>=0);
  swaggerApp.get(resourcePath, function sendSwaggerObject(req, res) {
    const definitions = {};
    const paths = {};
    const tags = [];
    const pModel = pluralize(model);
    map(swaggerObject.definitions, (item, key) => {
      if (key && (key.indexOf(model) >= 0 || key.indexOf(pModel) >= 0)) {
        definitions[key] = item;
      }
    });
    map(swaggerObject.tags, item => {
      if (item.name === model || item.name === pModel) {
        tags.push(item);
      }
    });
    map(swaggerObject.paths, (item, key) => {
      if (key && (key.indexOf(model) >= 0 || key.indexOf(pModel) >= 0)) {
        paths[key] = item;
      }
    });
    res.status(200).send({ ...swaggerObject, definitions, paths, tags });
  });
}
