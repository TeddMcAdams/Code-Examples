using System.Collections.Generic;
using System.Linq;
using HRPortal.Contracts.Repository;
using HRPortal.Data;
using HRPortal.Models;
using NUnit.Framework;

namespace HRPortal.Tests.RepositoryTests
{
    [TestFixture]
    public class PolicyRepoTests
    {
        private IPolicyRepository _polRepo;
        private List<Policy> _data;
        private Policy _testPolicy;

        [SetUp]
        public void TestSetup()
        {
            _polRepo = RepositoryFactory.GetPolicyRepository(true);
            _testPolicy = new Policy();
        }

        [TearDown]
        public void TestTearDown()
        {
            _data = _polRepo.LoadAll().ToList();
            foreach (Policy policy in _data.Where(p => p.PolicyId > 6))
            {
                _polRepo.Remove(policy.PolicyId);
            }
        }

        [Test]
        public void CanAddPolicy()
        {
            _testPolicy.CategoryId = 2;
            _testPolicy.Title = "TEST";
            var actual = _polRepo.Add(_testPolicy);

            Assert.AreEqual(7, actual.PolicyId);
            Assert.AreEqual("TEST", actual.Title);
            Assert.AreEqual(7, _polRepo.LoadAll().Count);
        }

        [Test]
        public void CanEditPolicy()
        {
            var policy = _polRepo.Load(1);
            policy.PolicyId = 20;
            policy.CategoryId = 3;

            var actual = _polRepo.Edit(1, policy);

            Assert.AreEqual("Internal Purchase Orders", actual.Title);
            Assert.AreEqual(1, actual.PolicyId);
            Assert.AreEqual(3, actual.CategoryId);
            Assert.AreEqual(6, _polRepo.LoadAll().Count);
        }

        [Test]
        public void CanLoadPolicies()
        {
            var actual = _polRepo.LoadAll().Count;

            Assert.AreEqual(6, actual);
        }

        [Test]
        public void CanLoadPolicy()
        {
            var actual = _polRepo.Load(2);

            Assert.AreEqual(2, actual.PolicyId);
            Assert.AreEqual("Travel Expenses", actual.Title);
        }

        [Test]
        public void CanRemovePolicy()
        {
            Policy testPol = _polRepo.Add(_testPolicy);

            Assert.AreEqual(7, _polRepo.LoadAll().Count);

            _polRepo.Remove(testPol.PolicyId);

            Assert.AreEqual(6, _polRepo.LoadAll().Count);
        }
    }
}