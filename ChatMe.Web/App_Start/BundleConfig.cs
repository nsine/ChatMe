using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;
using System.Web;

namespace ChatMe.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/base").Include(
                        "~/Scripts/jquery-{version}.js",
                         "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/posts-app").IncludeDirectory(
                        "~/Scripts/posts-app", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/dialogs-app").IncludeDirectory(
                        "~/Scripts/dialogs-app", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/user").IncludeDirectory(
                        "~/Scripts/User", "*.js", true));


            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/messages").Include("~/Content/messages.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                        "~/Content/bootstrap.css"));
        }
    }
}