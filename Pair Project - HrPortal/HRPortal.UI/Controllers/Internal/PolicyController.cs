using System.Web.Mvc;
using HRPortal.BLL;
using HRPortal.Contracts.Manager;
using HRPortal.Models;

namespace HRPortal.UI.Controllers.Internal
{
    public class PolicyController : Controller
    {
        private readonly IPolicyManager _polMgr;

        public PolicyController()
        {
            _polMgr = ManagerFactory.GetPolicyManager();
        }

        [HttpPost]
        public ActionResult Add(Policy policyToAdd)
        {
            _polMgr.Add(policyToAdd);

            return RedirectToAction("ManagePolicies", "Hr");
        }

        [HttpPost]
        public ActionResult Edit(int policyId, Policy policyToEdit)
        {
            _polMgr.Edit(policyId, policyToEdit);

            return RedirectToAction("ManagePolicies", "Hr");
        }

        [HttpPost]
        public ActionResult Remove(int policyId)
        {
            _polMgr.Remove(policyId);

            return RedirectToAction("ManagePolicies", "Hr");
        }
    }
}