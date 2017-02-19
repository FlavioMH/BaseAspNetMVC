using System.Web;
using System.Web.Optimization;

namespace BaseAspNetMvc.Web
{
    public class BundleConfig
    {
        /// <summary>
        ///     [REGISTERBUNDLES]
        ///     NOTE su bundle:
        ///     - min.css, min.js, debug.js, intellisense.hs e vsdoc.js di defailt sono ignorati (non vengono inseriti nella
        ///     pagina)
        ///     Per ulteriori informazioni sul Bundling, visitare il sito Web all'indirizzo
        ///     http://go.microsoft.com/fwlink/?LinkId=254725
        ///     Risorsa Utile 1 : http://msdn.microsoft.com/en-us/downloads/vs11trainingcourse_aspnetandvisualstudio_topic5.aspx
        ///     Risorsa Utile 2 : http://www.asp.net/mvc/tutorials/mvc-4/bundling-and-minification
        ///     Disable ignore on min : http://stackoverflow.com/a/12005272
        ///     Attenzione : {version} prende la versione piu alta MA lo fa in ordine alfabetico (su base regex) quindi 1.8.x
        ///     risulta erroneamente piu alto di 1.10.x
        /// </summary>
        /// <param name="bundles">Collection</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Bundle CSS

            bundles.Add(new StyleBundle(VirtualPaths.Style.Bootstrap).Include(
                "~/Content/bootstrap.css"));
            bundles.Add(new StyleBundle(VirtualPaths.Style.Site).Include(
                "~/Content/Site.css"));

            #endregion

            #region Bundle JAVASCRIPTS

            bundles.Add(new ScriptBundle(VirtualPaths.Script.Jquery).Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle(VirtualPaths.Script.JqueryValidate).Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle(VirtualPaths.Script.Modernizr).Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle(VirtualPaths.Script.BootstrapPack).Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            #endregion
        }
    }
}

namespace System.Web.Optimization
{
    public static class VirtualPaths
    {
        public static class Script
        {
            /// <summary>
            ///     "~/Scripts/bootstrap.js"
            ///     "~/Scripts/respond.js"  
            /// </summary>
            public const string BootstrapPack = @"~/js/bootstrapPack";
            /// <summary>
            ///     "~/Scripts/jquery-{version}.js"
            /// </summary>
            public const string Jquery = @"~/js/jquery";
            /// <summary>
            ///     "~/Scripts/jquery.validate"
            ///     "~/Scripts/jquery.validate.unobtrusive.js"
            ///     "~/Scripts/jquery.validate.unobtrusive.localization.js"
            /// </summary>
            public const string JqueryValidate = @"~/js/jqueryVal";
            /// <summary>
            ///     "~/Scripts/modernizr-2.8.3.js"
            /// </summary>
            public const string Modernizr = @"~/js/modernizr";
        }

        public static class Style
        {
            /// <summary>
            ///     "~/Content/bootstrap"     
            /// </summary>
            public const string Bootstrap = @"~/css/Bootstrap";
            /// <summary>
            ///     "~/Content/Site.css"
            /// </summary>
            public const string Site = @"~/css/site";                            
        }
    }
}
