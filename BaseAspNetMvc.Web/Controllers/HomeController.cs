using BaseAspNetMvc.Structure;
using BaseAspNetMvc.Structure.Controllers;
using BaseAspNetMvc.Web.WorkerServices;
using System.Web.Mvc;

namespace BaseAspNetMvc.Web.Controllers
{
    public class HomeController : BaseController<IHomeWorkerService>
    {
        public HomeController(IHomeWorkerService ws)
            : base(ws)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}