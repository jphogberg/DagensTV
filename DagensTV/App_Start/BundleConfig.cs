using System.Web;
using System.Web.Optimization;

namespace DagensTV
{
    public class BundleConfig
    {        
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {           
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js"));            

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));                                    

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            /* Mina knippen */

            bundles.UseCdn = true;

            var cdnPath1 = "https://fonts.googleapis.com/css?family=Roboto:300,400,700";
            var cdnPath2 = "https://fonts.googleapis.com/css?family=Roboto+Condensed:300,400,700";
            var cdnPath3 = "https://cdn.materialdesignicons.com/2.0.46/css/materialdesignicons.min.css";

            bundles.Add(new StyleBundle("~/font1", cdnPath1));
            bundles.Add(new StyleBundle("~/font2", cdnPath2));
            bundles.Add(new StyleBundle("~/icons", cdnPath3));
                

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                    "~/Content/themes/base/accordion.css",
                    "~/Content/themes/base/core.css",
                    "~/Content/themes/base/resizable.css",
                    "~/Content/themes/base/selectable.css",
                    "~/Content/themes/base/accordion.css",
                    "~/Content/themes/base/autocomplete.css",
                    "~/Content/themes/base/button.css",
                    "~/Content/themes/base/dialog.css",
                    "~/Content/themes/base/slider.css",
                    "~/Content/themes/base/tabs.css",
                    "~/Content/themes/base/datepicker.css",
                    "~/Content/themes/base/progressbar.css",
                    "~/Content/themes/base/theme.css"));

            bundles.Add(new ScriptBundle("~/bundles/myJS").Include(
                      "~/Scripts/myjquery.js",
                      "~/Scripts/autocomplete.js",
                      "~/Scripts/myjscript.js"));

            bundles.Add(new StyleBundle("~/Content/myCSS").Include(
                      "~/Content/reset.css",
                      "~/Content/style.css"));
        }
    }
}
