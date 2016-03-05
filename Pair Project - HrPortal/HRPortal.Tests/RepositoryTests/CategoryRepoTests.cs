using System.Collections.Generic;
using System.Linq;
using HRPortal.Contracts.Repository;
using HRPortal.Data;
using HRPortal.Models;
using NUnit.Framework;

namespace HRPortal.Tests.RepositoryTests
{
    [TestFixture]
    public class CategoryRepoTests
    {
        private ICategoryRepository _catRepo;
        private List<Category> _data;
        private Category _testCategory;

        [SetUp]
        public void TestSetup()
        {
            _catRepo = RepositoryFactory.GetCategoryRepository(true);
            _testCategory = new Category();
        }

        [TearDown]
        public void TestTearDown()
        {
            _data = _catRepo.LoadAll().ToList();
            foreach (Category category in _data.Where(category => category.CategoryId > 3))
            {
                _catRepo.Remove(category.CategoryId);
            }
        }

        [Test]
        public void CanAddCategory()
        {
            _testCategory.Title = "TEST";
            var actual = _catRepo.Add(_testCategory);

            Assert.AreEqual(4, actual.CategoryId);
            Assert.AreEqual("TEST", actual.Title);
            Assert.AreEqual(5, _catRepo.LoadAll().Count);
        }

        [Test]
        public void CanEditCategory()
        {
            var category = _catRepo.Load(1);
            category.Title = "Admin";

            var actual = _catRepo.Edit(1, category);

            Assert.AreEqual("Admin", actual.Title);
            Assert.AreEqual(1, actual.CategoryId);
            Assert.AreEqual(4, _catRepo.LoadAll().Count);
        }

        [Test]
        public void CanLoadCategories()
        {
            var actual = _catRepo.LoadAll().Count;

            Assert.AreEqual(4, actual);
        }

        [Test]
        public void CanLoadCategory()
        {
            var actual = _catRepo.Load(2);

            Assert.AreEqual(2, actual.CategoryId);
            Assert.AreEqual("Employee Conduct", actual.Title);
        }

        [Test]
        public void CanRemoveCategory()
        {
            Category testCat = _catRepo.Add(_testCategory);

            Assert.AreEqual(5, _catRepo.LoadAll().Count);

            _catRepo.Remove(testCat.CategoryId);

            Assert.AreEqual(4, _catRepo.LoadAll().Count);
        }

        [Test]
        public void CantRemoveCategoryZero()
        {
            _catRepo.Remove(0);

            Assert.AreEqual(4, _catRepo.LoadAll().Count);
        }
    }
}