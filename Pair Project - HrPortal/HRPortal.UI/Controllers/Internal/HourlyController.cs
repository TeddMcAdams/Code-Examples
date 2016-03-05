using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HRPortal.BLL;
using HRPortal.Contracts.Manager;
using HRPortal.Models;
using HRPortal.UI.Models;

namespace HRPortal.UI.Controllers.Internal
{
    public class HourlyController : Controller
    {
        private readonly ITimeSheetManager _tshMgr;
        private readonly ICategoryManager _catMgr;
        
        public HourlyController()
        {
            _tshMgr = ManagerFactory.GetTimeSheetManager();
            _catMgr = ManagerFactory.GetCategoryManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewPolicies()
        {
            Response<List<Category>> response = _catMgr.LoadAll();

            return response.Success
                ? (ActionResult)
                    View(
                        _catMgr.LoadAll()
                            .Data.Select(category => new CategoryWithItsPoliciesVm(category.CategoryId))
                            .ToList())
                : RedirectToAction("Index", "Hourly");
        }

        public ActionResult SubmitTime()
        {
            ViewData["alertDisplay"] = "none";
            ViewData["alertMsg"] = "";
            ViewData["alertType"] = "";
            return View(new TimeSheetEntryVm());
        }

        [HttpPost]
        public ActionResult SubmitTime(TimeSheetEntryVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            if (_tshMgr.LoadAll()
                    .Data.Select(ts => ts)
                    .Where(ts => ts.EmployeeId == vm.TimeEntry.EmployeeId)
                    .Select(d => d.Date)
                    .Contains(vm.TimeEntry.Date))
            {
                ViewData["alertType"] = "alert-danger";
                ViewData["alertMsg"] = "You have already entered a time sheet for this date.";
                ViewData["alertDisplay"] = "block";
                return View(vm);
            }

            var response = _tshMgr.Add(vm.TimeEntry);

            ViewData["alertType"] = response.Success ? "alert-success" : "alert-danger";
            ViewData["alertMsg"] = response.Message;
            ViewData["alertDisplay"] = "block";

            return View(vm);
        }
    }
}