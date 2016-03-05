using System;
using System.Collections.Generic;
using System.Linq;
using HRPortal.BLL;
using HRPortal.Contracts.Manager;
using HRPortal.Models;
using NUnit.Framework;

namespace HRPortal.Tests.ManagerTests
{
    [TestFixture]
    public class EmployeeMgrTests
    {
        private IEmployeeManager _empMgr;
        private List<Employee> _data;
        private Employee _testEmployee;

        [SetUp]
        public void TestSetup()
        {
            _empMgr = ManagerFactory.GetEmployeeManager(true);
            _testEmployee = new Employee();
        }

        [TearDown]
        public void TestTearDown()
        {
            _data = _empMgr.LoadAll().Data;
            foreach (Employee employee in _data.Where(e => e.EmployeeId > 8))
            {
                _empMgr.Remove(employee.EmployeeId);
            }
        }

        [Test]
        public void ManagerCanAddEmployee()
        {
            _testEmployee.FirstName = "TEST";
            _testEmployee.LastName = "TEST";
            _testEmployee.DateHired = DateTime.Now;

            var actual = _empMgr.Add(_testEmployee);

            Assert.AreEqual(9, actual.Data.EmployeeId);
            Assert.AreEqual(9, _empMgr.LoadAll().Data.Count);
        }

        [Test]
        public void ManagerCanLoadEmployee()
        {
            var actual = _empMgr.Load(1);

            Assert.IsTrue(actual.Success);
            Assert.AreEqual("Ted", actual.Data.FirstName);
        }

        [Test]
        public void ManagerCanLoadAllEmployees()
        {
            var actual = _empMgr.LoadAll().Data.Count;

            Assert.AreEqual(actual, 8);
        }
    }
}