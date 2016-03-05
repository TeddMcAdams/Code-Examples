using System.Collections.Generic;
using System.Linq;
using HRPortal.BLL;
using HRPortal.Contracts.Manager;
using HRPortal.Models;
using NUnit.Framework;

namespace HRPortal.Tests.ManagerTests
{
    [TestFixture]
    public class ApplicationMgrTests
    {
        private IApplicationManager _appMgr;
        private List<Application> _data;
        private Application _testApplication;

        [SetUp]
        public void TestSetup()
        {
            _appMgr = ManagerFactory.GetApplicationManager(true);
            _testApplication = new Application();
        }

        [TearDown]
        public void TestTearDown()
        {
            _data = _appMgr.LoadAll().Data;
            foreach (Application a in _data.Where(a => a.ApplicationId > 3))
            {
                _appMgr.Remove(a.ApplicationId);
            }
        }

        [Test]
        public void ManagerCanAddApplication()
        {
            _testApplication.FirstName = "Jake";
            _testApplication.LastName = "Smith";
            var actual = _appMgr.Add(_testApplication);

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(4, _appMgr.LoadAll().Data.Count);
        }

        [Test]
        public void ManagerCanLoadApplication()
        {
            var actual = _appMgr.Load(1);

            Assert.IsTrue(actual.Success);
            Assert.AreEqual("Bob", actual.Data.FirstName);
        }
    }
}