/**=========================================================
 * Module: TranslateConfig.js
 =========================================================*/

(function() {
    'use strict';

    angular
        .module('naut')
        .config(translateConfig);
    /* @ngInject */
    function translateConfig($translateProvider) {

      $translateProvider.useStaticFilesLoader({
        prefix: '/Langs/',
        suffix: '.json'
      });
      $translateProvider.preferredLanguage('en');
      $translateProvider.useLocalStorage();
    }
    translateConfig.$inject = ['$translateProvider'];

})();
