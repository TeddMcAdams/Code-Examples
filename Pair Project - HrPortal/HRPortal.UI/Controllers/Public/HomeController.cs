using System.Web.Mvc;
using HRPortal.BLL;
using HRPortal.Contracts.Manager;
using HRPortal.Models;

namespace HRPortal.UI.Controllers.Public
{
    public class HomeController : Controller
    {
        private readonly IApplicationManager _mgr;

        public HomeController()
        {
            _mgr = ManagerFactory.GetApplicationManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Apply()
        {
            ViewData["alertDisplay"] = "none";
            ViewData["alertMsg"] = "";
            ViewData["alertType"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult Apply(Application applicationToAdd)
        {

            var response = _mgr.Add(applicationToAdd);

            if (response.Success)
            {
                ViewData["alertType"] = "alert-success";
            }
            else
            {
                ViewData["alertType"] = "alert-danger";
            }
            ViewData["alertMsg"] = response.Message;
            ViewData["alertDisplay"] = "block";

            return View();
        }
    }
}