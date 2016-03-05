using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HRPortal.BLL;
using HRPortal.Contracts.Manager;
using HRPortal.Models;
using HRPortal.UI.Models;

namespace HRPortal.UI.Controllers.Internal
{
    public class HrController : Controller
    {
        private readonly IApplicationManager _appMgr;
        private readonly ICategoryManager _catMgr;
        private readonly IEmployeeManager _empMgr;

        public HrController()
        {
            _appMgr = ManagerFactory.GetApplicationManager();
            _catMgr = ManagerFactory.GetCategoryManager();
            _empMgr = ManagerFactory.GetEmployeeManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewApplications()
        {
            Response<List<Application>> response = _appMgr.LoadAll();

            return response.Success ? View(response.Data) : View("Index");
        }

        public ActionResult ViewPolicies()
        {
            Response<List<Category>> response = _catMgr.LoadAll();

            return response.Success
                ? View(
                    _catMgr.LoadAll()
                        .Data.Select(category => new CategoryWithItsPoliciesVm(category.CategoryId))
                        .ToList())
                : View("Index");
        }

        public ActionResult ManagePolicies()
        {
            Response<List<Category>> response = _catMgr.LoadAll();

            return response.Success
                ? View(
                    _catMgr.LoadAll()
                        .Data.Select(category => new CategoryWithItsPoliciesVm(category.CategoryId))
                        .ToList())
                : View("Index");
        }

        public ActionResult ViewTimeSheets()
        {
            var response = _empMgr.LoadAll();
            var empList = new List<EmployeeAndTimeSheetsVm>();
            
            if (response.Success)
            {
                empList.AddRange(response.Data.Select(employee => new EmployeeAndTimeSheetsVm(employee.EmployeeId)));
            }
            return View(empList);
        }
    }
}