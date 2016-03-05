using System.Collections.Generic;
using HRPortal.BLL.Managers;
using HRPortal.Models;

namespace HRPortal.UI.Models
{
    public class TimeSheetEntryVm
    {
        public TimeSheet TimeEntry { get; set; }
        public List<Employee> Employees { get; private set; }

        public TimeSheetEntryVm()
        {
            TimeEntry = new TimeSheet();
            var empMgr = new EmployeeManager();
            Employees = empMgr.LoadAll().Data;
        }
    }
}