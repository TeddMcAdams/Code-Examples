using System.Collections.Generic;
using System.Linq;
using HRPortal.BLL.Managers;
using HRPortal.Models;

namespace HRPortal.UI.Models
{
    public class CategoryWithItsPoliciesVm
    {
        public Category Category { get; private set; }
        public List<Policy> Policies { get; private set; }

        public CategoryWithItsPoliciesVm(int categoryId)
        {
            var ctrMgr = new CategoryManager();
            var polMgr = new PolicyManager();
            Category = ctrMgr.Load(categoryId).Data;
            Policies = polMgr.LoadAll().Data.Where(p => p.CategoryId == categoryId).ToList();
        }
    }
}