using System;
using System.Collections.Generic;
using System.Linq;
using HRPortal.Contracts.Manager;
using HRPortal.Contracts.Repository;
using HRPortal.Data;
using HRPortal.Models;

namespace HRPortal.BLL.Managers
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeManager()
        {
            _repo = RepositoryFactory.GetEmployeeRepository();
        }

        public EmployeeManager(bool nUnitTest)
        {
            _repo = RepositoryFactory.GetEmployeeRepository(nUnitTest);
        }

        public Response<Employee> Add(Employee employeeToAdd)
        {
            var response = new Response<Employee>();
            try
            {
                response.Success = true;
                response.Message = "The employee name was added to the database.";
                response.Data = _repo.Add(employeeToAdd);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Employee name could not be added to the database.";
            }
            return response;
        }

        public Response<Employee> Edit(int employeeId, Employee employeeToEdit)
        {
            var response = new Response<Employee>();
            try
            {
                response.Success = true;
                response.Message = "Edited employee.";
                response.Data = _repo.Edit(employeeId, employeeToEdit);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to edit employee.";
            }
            return response;
        }

        public Response<int> Remove(int employeeId)
        {
            var response = new Response<int>();
            try
            {
                response.Success = true;
                response.Message = "Removed employee.";
                response.Data = _repo.Remove(employeeId);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to remove employee.";
            }
            return response;
        }

        public Response<Employee> Load(int employeeId)
        {
            var response = new Response<Employee>();
            try
            {
                response.Success = true;
                response.Message = "Employee loaded.";
                response.Data = _repo.Load(employeeId);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to load empoyee.";
            }
            return response;
        }

        public Response<List<Employee>> LoadAll()
        {
            var  response = new Response<List<Employee>>();
            try
            {
                response.Success = true;
                response.Message = "Loaded all employees.";
                response.Data = _repo.LoadAll().OrderBy(e => e.LastName).ToList();
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to load employees.";
                throw;
            }
            return response;
        }
    }
}