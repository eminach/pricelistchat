/**=========================================================
 * Module: SettingsController.js
 * Handles app setting
 =========================================================*/

(function() {
    'use strict';

    angular
        .module('naut')
        .controller('SettingsController', SettingsController);
    /* @ngInject */
    function SettingsController($scope, settings) {

      // Restore/Save layout settings
      settings.loadAndWatch();

      // Set scope for panel settings
      $scope.themes = settings.availableThemes();
      $scope.setTheme = settings.setTheme;

    }
    SettingsController.$inject = ['$scope', 'settings'];
})();
