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
    public class TimeSheetMgrTests
    {
        private ITimeSheetManager _tshMgr;
        private List<TimeSheet> _data;
        private TimeSheet _testTimeSheet;

        [SetUp]
        public void TestSetup()
        {
            _tshMgr = ManagerFactory.GetTimeSheetManager(true);
            _testTimeSheet = new TimeSheet();
        }

        [TearDown]
        public void TestTearDown()
        {
            _data = _tshMgr.LoadAll().Data;
            foreach (TimeSheet timeSheet in _data.Where(ts => ts.TimeSheetId > 1))
            {
                _tshMgr.Remove(timeSheet.TimeSheetId);
            }
        }

        [Test]
        public void CanAddTimeSheet()
        {
            _testTimeSheet.EmployeeId = 2;
            _testTimeSheet.HoursWorked = 2M;
            _testTimeSheet.Date = DateTime.Now;
            _tshMgr.Add(_testTimeSheet);

            Assert.AreEqual(2, _tshMgr.LoadAll().Data.Count);
        }
    }
}