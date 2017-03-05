/**=========================================================
 * Module: ColorsConstant.js
 =========================================================*/

(function() {
  'use strict';

  // Same MQ as defined in the css
  angular
    .module('naut')
    .constant('MEDIA_QUERY', {
      'desktopLG': 1200,
      'desktop':   992,
      'tablet':    768,
      'mobile':    480
    });

})();
