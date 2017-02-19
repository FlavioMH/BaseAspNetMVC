using System.Web.Mvc;

namespace BaseAspNetMvc.Structure.Views
{
    public class BaseViewEngine
    {
        public static void RegisterViewEngines()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CSharpRazorViewEngine());
        }
    }

    public class CSharpRazorViewEngine : RazorViewEngine
    {
        public CSharpRazorViewEngine()
        {
            AreaViewLocationFormats = new[]
                {
                    "~/Areas/{2}/Views/{1}/{0}.cshtml",
                    "~/Areas/{2}/Views/Shared/{0}.cshtml"
                };
            AreaMasterLocationFormats = new[]
                {
                    "~/Areas/{2}/Views/{1}/{0}.cshtml",
                    "~/Areas/{2}/Views/Shared/{0}.cshtml"
                };
            AreaPartialViewLocationFormats = new[]
                {
                    "~/Areas/{2}/Views/{0}/{0}.cshtml",// controller with view name
                    "~/Areas/{2}/Views/{1}/{0}.cshtml",
                    "~/Areas/{2}/Views/Shared/{0}.cshtml",
                    "~/Areas/{2}/Views/{1}/EditorTemplates/{0}.cshtml"
                };
            ViewLocationFormats = new[]
                {
                    "~/Views/{1}/{0}.cshtml",
                    "~/Views/Shared/{0}.cshtml"
                };
            MasterLocationFormats = new[]
                {
                    "~/Views/{1}/{0}.cshtml",
                    "~/Views/Shared/{0}.cshtml"
                };
            PartialViewLocationFormats = new[]
                {
                    "~/Views/{1}/{0}.cshtml",
                    "~/Views/Shared/{0}.cshtml"
                };
        }
    }
}