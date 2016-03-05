using System.Web.Hosting;
using HRPortal.Contracts.Repository;
using HRPortal.Data.Repositories;

namespace HRPortal.Data
{
    public static class RepositoryFactory
    {
        public static IApplicationRepository GetApplicationRepository()
        {
            return new ApplicationRepository(HostingEnvironment.MapPath("/App_Data/ApplicationTable.xml"));
        }

        public static IApplicationRepository GetApplicationRepository(bool nUnitTest)
        {
            return nUnitTest ? new ApplicationRepository("App_Data/ApplicationTable.xml") : new ApplicationRepository("");
        }

        public static ICategoryRepository GetCategoryRepository()
        {
            return new CategoryRepository(HostingEnvironment.MapPath("/App_Data/CategoryTable.xml"));
        }

        public static ICategoryRepository GetCategoryRepository(bool nUnitTest)
        {
            return nUnitTest ? new CategoryRepository("App_Data/CategoryTable.xml") : new CategoryRepository("");
        }

        public static IEmployeeRepository GetEmployeeRepository()
        {
            return new EmployeeRepository(HostingEnvironment.MapPath("/App_Data/EmployeeTable.xml"));
        }

        public static IEmployeeRepository GetEmployeeRepository(bool nUnitTest)
        {
            return nUnitTest ? new EmployeeRepository("App_Data/EmployeeTable.xml") : new EmployeeRepository("");
        }

        public static IPolicyRepository GetPolicyRepository()
        {
            return new PolicyRepository(HostingEnvironment.MapPath("/App_Data/PolicyTable.xml"));
        }

        public static IPolicyRepository GetPolicyRepository(bool nUnitTest)
        {
            return nUnitTest ? new PolicyRepository("App_Data/PolicyTable.xml") : new PolicyRepository("");
        }

        public static ITimeSheetRepository GetTimeSheetRepository()
        {
            return new TimeSheetRepository(HostingEnvironment.MapPath("/App_Data/TimeSheetTable.xml"));
        }

        public static ITimeSheetRepository GetTimeSheetRepository(bool nUnitTest)
        {
            return nUnitTest ? new TimeSheetRepository("App_Data/TimeSheetTable.xml") : new TimeSheetRepository("");
        }
    }
}