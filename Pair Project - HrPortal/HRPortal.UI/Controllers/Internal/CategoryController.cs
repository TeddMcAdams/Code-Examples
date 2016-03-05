using System.Web.Mvc;
using HRPortal.BLL;
using HRPortal.Contracts.Manager;
using HRPortal.Models;

namespace HRPortal.UI.Controllers.Internal
{
    public class CategoryController : Controller
    {
        private readonly ICategoryManager _catMgr;

        public CategoryController()
        {
            _catMgr = ManagerFactory.GetCategoryManager();
        }

        [HttpPost]
        public ActionResult Add(Category categoryToAdd)
        {
            Response<Category> response = _catMgr.Add(categoryToAdd);

            return RedirectToAction(response.Success ? "ManagePolicies" : "Index", "Hr");
        }

        [HttpPost]
        public ActionResult Edit(int categoryId, Category categoryToEdit)
        {
            Response<Category> response = _catMgr.Edit(categoryId, categoryToEdit);

            return RedirectToAction(response.Success ? "ManagePolicies" : "Index", "Hr");
        }

        [HttpPost]
        public ActionResult Remove(int categoryId)
        {
            Response<int> response = _catMgr.Remove(categoryId);

            return RedirectToAction(response.Success ? "ManagePolicies" : "Index", "Hr");
        }
    }
}