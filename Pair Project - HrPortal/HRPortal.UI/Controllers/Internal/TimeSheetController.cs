using System.Web.Mvc;
using HRPortal.BLL;
using HRPortal.Contracts.Manager;
using HRPortal.Models;

namespace HRPortal.UI.Controllers.Internal
{
    public class TimeSheetController : Controller
    {
        private readonly ITimeSheetManager _tshMgr;

        public TimeSheetController()
        {
            _tshMgr = ManagerFactory.GetTimeSheetManager();
        }

        [HttpPost]
        public ActionResult Remove(int timeSheetId)
        {
            Response<int> response = _tshMgr.Remove(timeSheetId);

            return RedirectToAction(response.Success ? "ViewTimeSheets" : "Index", "Hr");
        }
    }
}