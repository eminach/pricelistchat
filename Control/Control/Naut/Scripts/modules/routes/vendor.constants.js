/**=========================================================
 * Module: VendorAssetsConstant.js
 =========================================================*/

(function() {
    'use strict';

    angular
        .module('naut')
        .constant('VENDOR_ASSETS', {
            // jQuery based and standalone scripts
            scripts: {
              'animate':            ['/Vendor/animate.css/animate.min.css'],
              'icons':              ['/Vendor/font-awesome/css/font-awesome.min.css',
                                     '/Vendor/weather-icons/css/weather-icons.min.css',
                                     '/Vendor/feather/webfont/feather-webfont/feather.css']
            },
            // Angular modules scripts (name is module name to be injected)
            modules: [
              {name: 'toaster',           files: ['/Vendor/angularjs-toaster/toaster.js',
                                                  '/Vendor/angularjs-toaster/toaster.css']
              }
            ]

        });

})();

