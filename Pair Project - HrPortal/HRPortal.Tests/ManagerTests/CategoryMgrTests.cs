using System.Collections.Generic;
using System.Linq;
using HRPortal.BLL;
using HRPortal.Contracts.Manager;
using HRPortal.Models;
using NUnit.Framework;

namespace HRPortal.Tests.ManagerTests
{
    [TestFixture]
    public class CategoryMgrTests
    {
        private ICategoryManager _catMgr;
        private List<Category> _data;
        private Category _testCategory;

        [SetUp]
        public void TestSetup()
        {
            _catMgr = ManagerFactory.GetCategoryManager(true);
            _testCategory = new Category();
        }

        [TearDown]
        public void TestTearDown()
        {
            _data = _catMgr.LoadAll().Data;
            foreach (Category c in _data.Where(c => c.CategoryId > 3))
            {
                _catMgr.Remove(c.CategoryId);
            }
        }

        [Test]
        public void MgrCanAddCategory()
        {
            _testCategory.Title = "TEST";
            var actual = _catMgr.Add(_testCategory);
        
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(5, _catMgr.LoadAll().Data.Count);
        }

        [Test]
        public void MgrCanLoadCategory()
        {
            var actual = _catMgr.Load(1);

            Assert.IsTrue(actual.Success);
            Assert.AreEqual("Administrative", actual.Data.Title);
        }
    }
}