using System.Collections.Generic;
using System.Linq;
using HRPortal.BLL.Managers;
using HRPortal.Models;

namespace HRPortal.UI.Models
{
    public class EmployeeAndTimeSheetsVm
    {
        public Employee Employee { get; private set; }
        public List<TimeSheet> TimeSheets { get; private set; }

        public EmployeeAndTimeSheetsVm(int employeeId)
        {
            var empMgr = new EmployeeManager();
            var tshMgr = new TimeSheetManager();
            Employee = empMgr.Load(employeeId).Data;
            TimeSheets = tshMgr.LoadAll().Data.Where(t => t.EmployeeId == employeeId).OrderByDescending(t => t.Date).ToList();
        }
    }
}