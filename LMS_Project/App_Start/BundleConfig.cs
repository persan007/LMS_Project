using System.Web;
using System.Web.Optimization;

namespace LMS_Project
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862

        public static void RegisterBundles(BundleCollection bundles)
        {
            // Load Modernizer
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"
            ));

            // Load Angular and page min file using GruntJS //
            bundles.Add(new ScriptBundle("~/bundles/core").Include(
                "~/project.angular.min.js",
                "~/project.core.min.js"
            ));

            // Load jQuery and Bootstrap min file using GruntJS //
            bundles.Add(new ScriptBundle("~/bundles/vendor").Include(
                "~/project.vendor.min.js"
            ));

            // Load page stylesheets
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/style.css"
            ));
        }
    }
}
