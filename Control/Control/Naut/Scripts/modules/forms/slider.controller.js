/**=========================================================
 * Module: SliderController.js
 * Initializes the fielstyle plugin
 =========================================================*/
(function() {
    'use strict';

    angular
        .module('naut')
        .controller('SliderController', SliderController);
    /* @ngInject */
    function SliderController($scope) {
      
      // Set initial values
      $scope.value = 50;
      $scope.range = {
        min: 20,
        max: 80
      };
      $scope.formatted = 30;
      $scope.range2 = {
        min: 20,
        max: 80
      };
      // return the value converted
      $scope.priceFormat = function(value) {
        return '$' + value.toString();
      };

    }
    SliderController.$inject = ['$scope'];
})();
