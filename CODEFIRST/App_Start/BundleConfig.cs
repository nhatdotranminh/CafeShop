using System.Web;
using System.Web.Optimization;

namespace CODEFIRST
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.js"));
           

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new StyleBundle("~/bundles/uiToTop").Include(
                    "~/Scripts/jquery.ui.totop.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/jquerymigrate").Include(
                    "~/Scripts/jquery-migrate-1.1.1.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryequalheights").Include(
                    "~/Scripts/js/jquery.equalheights.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryeasing").Include(
                   "~/Scripts/jquery.easing.1.3.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/tab").Include(
                "~/Scripts/jquery.tabs.min.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/touchTouch").Include(
                "~/Scripts/touchTouch.jquery.js"
                ));




    


              // Use the development version of Modernizr to develop with and learn from. Then, when you're
              // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
              bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/style.css",
                     
                      "~/Content/css/menu.css"      
                      ));
            bundles.Add(new StyleBundle("~/Content/admincss").Include(
                     "~/Content/bootstrap.css",
                     "~/Content/Site.css"));

        }
    }
}
