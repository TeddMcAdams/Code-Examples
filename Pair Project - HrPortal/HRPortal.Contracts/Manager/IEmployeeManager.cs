using System.Collections.Generic;
using HRPortal.Models;

namespace HRPortal.Contracts.Manager
{
    public interface IEmployeeManager
    {
        Response<Employee> Add(Employee employeeToAdd);
        Response<Employee> Edit(int employeeId, Employee employeeToEdit);
        Response<int> Remove(int employeeId);
        Response<Employee> Load(int employeeId);
        Response<List<Employee>> LoadAll();
    }
}