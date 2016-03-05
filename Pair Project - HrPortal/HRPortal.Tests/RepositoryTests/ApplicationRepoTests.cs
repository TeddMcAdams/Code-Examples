using System.Collections.Generic;
using System.Linq;
using HRPortal.Contracts.Repository;
using HRPortal.Data;
using HRPortal.Models;
using NUnit.Framework;

namespace HRPortal.Tests.RepositoryTests
{
    [TestFixture]
    public class ApplicationRepoTests
    {
        private IApplicationRepository _appRepo;
        private List<Application> _data;
        private Application _testApplication;

        [SetUp]
        public void TestSetup()
        {
            _appRepo = RepositoryFactory.GetApplicationRepository(true);
            _testApplication = new Application();
        }

        [TearDown]
        public void TestTearDown()
        {
            _data = _appRepo.LoadAll().ToList();
            foreach (Application application in _data.Where(a => a.ApplicationId > 3))
            {
                _appRepo.Remove(application.ApplicationId);
            }
        }

        [Test]
        public void RepoCanLoadApplication()
        {
            var actual = _appRepo.Load(1);

            Assert.AreEqual("Bob", actual.FirstName);
        }

        [Test]
        public void RepoCanAddApplication()
        {
            _testApplication.FirstName = "John";
            _testApplication.LastName = "Stanton";
            _testApplication.Email = "Blah@g.com";
            _testApplication.Phone = "2165555777";
            _testApplication.LinkedIn = "google.com";
            _testApplication.WhyInterested = "yep";

            _appRepo.Add(_testApplication);

            Assert.AreEqual(4, _appRepo.LoadAll().Count);
        }

        [Test]
        public void RepoCanRemoveApplication()
        {
            _appRepo.Remove(2);

            Assert.AreEqual(2, _appRepo.LoadAll().Count);
        }

        [Test]
        public void RepoCanEditApplication()
        {
            _testApplication.ApplicationId = 20;
            _testApplication.FirstName = "John";
            _testApplication.LastName = "Stanton";
            _testApplication.Email = "Blah@g.com";
            _testApplication.Phone = "2165555777";
            _testApplication.LinkedIn = "google.com";
            _testApplication.WhyInterested = "yep";

            _appRepo.Edit(3, _testApplication);

            Assert.AreEqual("John", _appRepo.Load(3).FirstName);
        }
    }
}