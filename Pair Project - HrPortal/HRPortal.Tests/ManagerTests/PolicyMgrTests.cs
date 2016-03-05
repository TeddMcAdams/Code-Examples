using System.Collections.Generic;
using System.Linq;
using HRPortal.BLL;
using HRPortal.Contracts.Manager;
using HRPortal.Models;
using NUnit.Framework;

namespace HRPortal.Tests.ManagerTests
{
    [TestFixture]
    public class PolicyMgrTests
    {
        private IPolicyManager _polMgr;
        private List<Policy> _data;
        private Policy _testPolicy;

        [SetUp]
        public void TestSetup()
        {
            _polMgr = ManagerFactory.GetPolicyManager(true);
            _testPolicy = new Policy();
        }

        [TearDown]
        public void TestTearDown()
        {
            _data = _polMgr.LoadAll().Data;
            foreach (Policy p in _data.Where(p => p.PolicyId > 6))
            {
                _polMgr.Remove(p.PolicyId);
            }
        }

        [Test]
        public void MgrCanAddPolicy()
        {
            _testPolicy.CategoryId = 0;
            _testPolicy.Title = "TEST";
            _testPolicy.Content = "TESTING";

            var actual = _polMgr.Add(_testPolicy);

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(7, _polMgr.LoadAll().Data.Count);
        }

        [Test]
        public void MgrCanLoadPolicy()
        {
            var actual = _polMgr.Load(2);

            Assert.IsTrue(actual.Success);
            Assert.AreEqual("Travel Expenses", actual.Data.Title);
        }
    }
}