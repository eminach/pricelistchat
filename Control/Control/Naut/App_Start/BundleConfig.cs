using System.Web;
using System.Web.Optimization;

namespace Naut
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Bootstrap Styles
            bundles.Add(new StyleBundle("~/bundles/BootstrapStyles").Include(
                      "~/Content/bootstrap.css"));
            // Application Styles
            bundles.Add(new StyleBundle("~/bundles/ApplicationStyles").Include(
                      "~/Content/styles.css"));
            // Application Script
            bundles.Add(new ScriptBundle("~/bundles/ApplicationScripts")
                .Include("~/Scripts/app.module.js")
                .IncludeDirectory("~/Scripts/modules", "*.js", true)
            );

            //----------------------
            // Vendor Bundle
            //----------------------

            bundles.Add(new ScriptBundle("~/bundles/VendorScripts").Include(
              "~/Vendor/jquery/dist/jquery.js",
              "~/Vendor/angular/angular.js",
              "~/Vendor/angular-animate/angular-animate.js",
              "~/Vendor/angular-bootstrap/ui-bootstrap-tpls.js",
              "~/Vendor/angular-cookies/angular-cookies.js",
              "~/Vendor/angular-dynamic-locale/dist/tmhDynamicLocale.js",
              "~/Vendor/angular-loading-bar/build/loading-bar.js",
              "~/Vendor/angular-resource/angular-resource.js",
              "~/Vendor/angular-route/angular-route.js",
              "~/Vendor/angular-sanitize/angular-sanitize.js",
              "~/Vendor/angular-touch/angular-touch.js",
              "~/Vendor/angular-translate/angular-translate.js",
              "~/Vendor/angular-translate-loader-static-files/angular-translate-loader-static-files.js",
              "~/Vendor/angular-translate-loader-url/angular-translate-loader-url.js",
              "~/Vendor/angular-translate-storage-cookie/angular-translate-storage-cookie.js",
              "~/Vendor/angular-translate-storage-local/angular-translate-storage-local.js",
              "~/Vendor/angular-ui-router/release/angular-ui-router.js",
              "~/Vendor/angular-ui-utils/ui-utils.js",
              "~/Vendor/modernizr/modernizr.js",
              "~/Vendor/ngstorage/ngStorage.js",
              "~/Vendor/oclazyload/dist/ocLazyLoad.js"
            ));


        }
    }
}
