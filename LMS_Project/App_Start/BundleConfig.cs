using System.Web;
using System.Web.Optimization;

namespace LMS_Project
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862

        public static void RegisterBundles(BundleCollection bundles)
        {
            // Load jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"
            ));

            // Load jQuery Validate
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"
            ));

            // Load Modernizer
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"
            ));

            // Load Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"
            ));

            // Load Angular with custom controllers
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular.js",
                "~/LMS/*.js",
                "~/LMS/Filters/*.js",
                "~/LMS/Providers/*.js",
                "~/LMS/Controllers/*.js"
            ));

            // Load page stylesheets
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"
            ));
        }
    }
}
