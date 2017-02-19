using System.Web;
using System.Web.Mvc;

namespace BaseAspNetMvc.Structure.UnityDependencyInjection
{
    public class BaseHttpApplication : HttpApplication
    {
        static BaseHttpApplication()
        {
            //  MVC Controller Factory
            var factory = new BaseUnityControllerFactory();
            ControllerBuilder.Current.SetControllerFactory((IControllerFactory)factory);
        }
    }
}
