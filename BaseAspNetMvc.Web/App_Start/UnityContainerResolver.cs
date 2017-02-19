using System;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using BaseAspNetMvc.Web.Controllers;
using BaseAspNetMvc.Structure.UnityDependencyInjection;
using BaseAspNetMvc.Web.WorkerServices;
using BaseAspNetMvc.Web.Models;
using BaseAspNetMvc.Structure.WorkerServices;
using BaseAspNetMvc.Structure.Providers;

namespace BaseAspNetMvc.Web.App_Start
{
    public class UnityContainerResolver
    {
        public static void ConfigureUnityContainerResolver(ref IUnityContainer container)
        {
            #region Account ASP.NET Identity
            container.RegisterType(
                typeof(AccountController),
                typeof(AccountController),
                "AccountController",
                new PerCallContextOrRequestLifeTimeManager(),
                new InjectionConstructor(typeof(ApplicationUserManager), typeof(ApplicationSignInManager), typeof(AccountWorkerService)));

            container.RegisterType(
                typeof(ManageController),
                typeof(ManageController),
                "ManageController",
                new PerCallContextOrRequestLifeTimeManager(),
                new InjectionConstructor(typeof(ApplicationUserManager), typeof(AccountWorkerService)));

            //container.RegisterType<IUserStore<ApplicationUser, int>, ApplicationUserStore>(
            //    new InjectionConstructor(typeof(ApplicationDbContext)));

            container.RegisterType<IAuthenticationManager>(
                new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            #endregion

            #region WorkerService
            container.RegisterType(
                typeof(EmptyWorkerService),
                typeof(EmptyWorkerService),
                "EmptyWorkerService",
                new PerCallContextOrRequestLifeTimeManager());            
            #endregion

            #region Providers
            container.RegisterType<IEmptyProvider, EmptyProvider>();
            #endregion
        }
    }
}
