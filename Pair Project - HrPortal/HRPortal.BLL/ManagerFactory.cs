using HRPortal.BLL.Managers;
using HRPortal.Contracts.Manager;

namespace HRPortal.BLL
{
    public static class ManagerFactory
    {
        public static IApplicationManager GetApplicationManager()
        {
            return new ApplicationManager();
        }

        public static IApplicationManager GetApplicationManager(bool nUnitTest)
        {
            return new ApplicationManager(nUnitTest);
        }

        public static ICategoryManager GetCategoryManager()
        {
            return new CategoryManager();
        }

        public static ICategoryManager GetCategoryManager(bool nUnitTest)
        {
            return new CategoryManager(nUnitTest);
        }

        public static IEmployeeManager GetEmployeeManager()
        {
            return new EmployeeManager();
        }

        public static IEmployeeManager GetEmployeeManager(bool nUnitTest)
        {
            return new EmployeeManager(nUnitTest);
        }

        public static IPolicyManager GetPolicyManager()
        {
            return new PolicyManager();
        }

        public static IPolicyManager GetPolicyManager(bool nUnitTest)
        {
            return new PolicyManager(nUnitTest);
        }

        public static ITimeSheetManager GetTimeSheetManager()
        {
            return new TimeSheetManager();
        }

        public static ITimeSheetManager GetTimeSheetManager(bool nUnitTest)
        {
            return new TimeSheetManager(nUnitTest);
        }
    }
}