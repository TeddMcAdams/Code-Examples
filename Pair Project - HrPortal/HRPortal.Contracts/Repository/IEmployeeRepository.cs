using System.Collections.Generic;
using HRPortal.Models;

namespace HRPortal.Contracts.Repository
{
    public interface IEmployeeRepository
    {
        Employee Add(Employee employeeToAdd);
        Employee Edit(int employeeId, Employee employeeToEdit);
        int Remove(int employeeId);
        Employee Load(int employeeId);
        List<Employee> LoadAll();
    }
}