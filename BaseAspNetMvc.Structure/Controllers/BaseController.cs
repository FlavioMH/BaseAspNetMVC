using BaseAspNetMvc.Structure.Utilities;
using log4net;
using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace BaseAspNetMvc.Structure.Controllers
{
    public class BaseController : Controller, IDisposable
    {
        protected readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected bool IsDisposed = false;
        protected string CultureCookieName = ConfigurationManager.AppSettings["CultureCookieName"];
        protected bool IsCultureRouteEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["IsCultureRouteEnabled"]);

        public BaseController()
        {
        }

        /// <summary>
        /// Overridden to take care of UI culture.
        /// There are 2 possible ways to activate culture control:
        /// 1) Adding culture code to the URl and setting IsCultureRouteEnabled config to true in thw web.config
        /// 2) Using cookie culture 
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            if(IsCultureRouteEnabled)
            {
                cultureName = RouteData.Values["culture"] as string;
                // Attempt to read the culture from Route Request
                if (cultureName == null)
                {
                    cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
                        Request.UserLanguages[0] :
                        null; // obtain it from HTTP header AcceptLanguages
                }
                    
                // Validate culture name
                cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe
                
                if (RouteData.Values["culture"] as string != cultureName)
                {
                    // Force a valid culture in the URL
                    RouteData.Values["culture"] = cultureName.ToLowerInvariant(); // lower case too
                    // Redirect user
                    Response.RedirectToRoute(RouteData.Values);
                }
            }
            else
            {
                // Attempt to read the culture cookie from Request
                HttpCookie cultureCookie = Request.Cookies[CultureCookieName];
                if (cultureCookie != null)
                    cultureName = cultureCookie.Value;
                else
                    cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
                            Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
                            null;
                // Validate culture name
                cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe
            }

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }

        /// <summary>
        /// Set selected culture to the current thread.
        /// If no action or controller names has been specified, just read them from the actual controllerContext
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public virtual ActionResult SetCulture(string culture)
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            return SetCultureWithControllerAndActionNames(culture, controllerName, actionName);
        }

        /// <summary>
        /// Set selected culture to the current thread and redirect to ControllerName/ActionName route
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public virtual ActionResult SetCultureWithControllerAndActionNames(string culture, string controllerName, string actionName)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);

            if(IsCultureRouteEnabled)
            {
                // Set culture
                RouteData.Values["culture"] = culture;  
            }
            else
            {
                // Save culture in a cookie
                HttpCookie cookie = Request.Cookies[CultureCookieName];
                if (cookie != null)
                    cookie.Value = culture;   // update cookie value
                else
                {
                    cookie = new HttpCookie(CultureCookieName);
                    cookie.Value = culture;
                    cookie.Expires = DateTime.Now.AddYears(1);
                }
                Response.Cookies.Add(cookie);
            }

            return RedirectToAction(actionName, controllerName, RouteData.Values);
        }

        // Find a view with name equal to "viewName" and render it in a string (also with model if it is defined in there)
        protected virtual string RenderPartialViewToString(string viewName, object model = null)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            if (model != null)
            {
                ViewData.Model = model;
            }

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        protected virtual JsonResult GetJson(bool success, string msg, object content)
        {
            return Json(new
            {
                Success = success,
                Message = msg,
                Content = content,
            }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            base.Dispose(disposing);
            IsDisposed = true;
        }
    }

    public class BaseController<T> : BaseController, IDisposable
    {
        protected T WorkerService { get; private set; } 

        public BaseController(T workerService)
        {
            if (workerService != null)
                WorkerService = workerService;
        }

        protected override void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            if (disposing && WorkerService is IDisposable)
                (WorkerService as IDisposable).Dispose();

            base.Dispose(disposing);
            IsDisposed = true;
        }
    }
}
