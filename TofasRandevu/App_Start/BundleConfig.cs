using System.Web;
using System.Web.Optimization;

namespace TofasRandevu
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Bundles/jquery").Include(
                        "~/assets/js/jquery-1.12.4.min.js"));

            bundles.Add(new ScriptBundle("~/Bundles/jqueryval").Include(
                        "~/assets/js/jquery.validate.min.js",
                        "~/assets/js/jquery.validate-vsdoc.js",
                        "~/assets/js/jquery.validate.unobtrusive.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/Bundles/mask").Include(
                  "~/assets/js/jquery.mask.min.js"));

            bundles.Add(new ScriptBundle("~/Bundles/scripts").Include(
                  "~/assets/js/scripts.js"));

            bundles.Add(new ScriptBundle("~/Bundles/placeholder").Include(
                  "~/assets/js/placeholder.js"));

            bundles.Add(new ScriptBundle("~/Bundles/bootstrap").Include(
                      "~/assets/bootstrap/js/bootstrap.min.js",
                      "~/assets/js/bootstrap-toggle.min.js",
                      "~/assets/js/moment-with-locales.min.js",
                      "~/assets/js//bootstrap-datetimepicker.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Contents/css").Include(
                      "~/assets/bootstrap/css/bootstrap.min.css",
                      "~/assets/font-awesome/css/font-awesome.min.css",
                      "~/assets/css/bootstrap-datetimepicker.css",
                      "~/assets/css/form-elements.css",
                      "~/assets/css/style.css"
                      ));
        }
    }
}
