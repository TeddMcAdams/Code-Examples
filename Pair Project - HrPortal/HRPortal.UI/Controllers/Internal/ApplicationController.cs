using System.Web.Mvc;
using HRPortal.BLL;
using HRPortal.Contracts.Manager;
using HRPortal.Models;

namespace HRPortal.UI.Controllers.Internal
{
    public class ApplicationController : Controller
    {
        private readonly IApplicationManager _appMgr;

        public ApplicationController()
        {
            _appMgr = ManagerFactory.GetApplicationManager();
        }

        [HttpPost]
        public ActionResult Edit(int applicationId, Application applicationToEdit)
        {
            Response<Application> response = _appMgr.Edit(applicationId, applicationToEdit);

            return RedirectToAction(response.Success ? "ViewApplications" : "Index", "Hr");
        }

        [HttpPost]
        public ActionResult Remove(int applicationId)
        {
            Response<int> resposne = _appMgr.Remove(applicationId);

            return RedirectToAction(resposne.Success ? "ViewApplications" : "Index", "Hr");
        }
    }
}