/**=========================================================
 * Module: HeaderNavController
 * Controls the header navigation
 =========================================================*/

(function() {
    'use strict';

    angular
        .module('naut')
        .controller('HeaderNavController', HeaderNavController);
    /* @ngInject */    
    function HeaderNavController($scope, $rootScope) {

      $scope.headerMenuCollapsed = true;

      $scope.toggleHeaderMenu = function() {
        $scope.headerMenuCollapsed = !$scope.headerMenuCollapsed;
      };

      // Adjustment on route changes
      $rootScope.$on('$stateChangeStart', function(event, toState, toParams, fromState, fromParams) {
        $scope.headerMenuCollapsed = true;
      });

    }
    HeaderNavController.$inject = ['$scope', '$rootScope'];

})();
