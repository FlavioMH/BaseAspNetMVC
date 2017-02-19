using BaseAspNetMvc.Structure;
using BaseAspNetMvc.Structure.UnityDependencyInjection;
using BaseAspNetMvc.Structure.Views;
using Microsoft.Practices.Unity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BaseAspNetMvc.Web
{
    public class MvcApplication : BaseHttpApplication
    {
        protected void Application_Start()
        {   
            // UNITY Container Configuration Definition
            IUnityContainer unityContainer = UnityContainerRepository.Container;
            //BaseUnityContainerResolver.ConfigureUnityContainerResolver(ref unityContainer);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // Add only CSHTML Razor View Engine
            BaseViewEngine.RegisterViewEngines();
        }
    }
}
