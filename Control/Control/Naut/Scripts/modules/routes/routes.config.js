/**=========================================================
 * Module: RoutesConfig.js
 =========================================================*/

(function() {
    'use strict';

    angular
        .module('naut')
        .config(routesConfig);

    routesConfig.$inject = ['$locationProvider', '$stateProvider', '$urlRouterProvider', 'RouteProvider'];
    function routesConfig($locationProvider, $stateProvider, $urlRouterProvider, Route) {

      // use the HTML5 History API
      $locationProvider.html5Mode(false);

      // Default route
      $urlRouterProvider.otherwise('/Dashboard');

      // Application Routes States
      $stateProvider
        .state('app', {
            abstract: true,
            controller: "CoreController",
            resolve: {
                _assets: Route.require('icons', 'toaster', 'animate')
            }
        })
        .state('app.dashboard', {
            url: '/Dashboard',
            templateUrl: Route.base('Dashboard/Index'),
            resolve: {}
        });
    }

})();

