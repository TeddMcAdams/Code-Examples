using System;
using System.Collections.Generic;
using System.Linq;
using HRPortal.Contracts.Manager;
using HRPortal.Contracts.Repository;
using HRPortal.Data;
using HRPortal.Models;

namespace HRPortal.BLL.Managers
{
    public class ApplicationManager : IApplicationManager
    {
        private readonly IApplicationRepository _repo;

        public ApplicationManager()
        {
            _repo = RepositoryFactory.GetApplicationRepository();
        }

        public ApplicationManager(bool nUnitTest)
        {
            _repo = RepositoryFactory.GetApplicationRepository(nUnitTest);
        }

        public Response<Application> Add(Application applicationToAdd)
        {
            var response = new Response<Application>();
            try
            {
                response.Success = true;
                response.Message = "Your application was accepted, thank you.";
                response.Data = _repo.Add(applicationToAdd);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Application could not be processed.  Please try again later.";
            }
            return response;
        }

        public Response<Application> Edit(int applicationId, Application applicationToEdit)
        {
            var response = new Response<Application>();
            try
            {
                response.Success = true;
                response.Message = "Edited application.";
                response.Data = _repo.Edit(applicationId, applicationToEdit);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to edit application.";
            }
            return response;
        }

        public Response<int> Remove(int applicationId)
        {
            var response = new Response<int>();
            try
            {
                response.Success = true;
                response.Message = "Removed application.";
                response.Data = _repo.Remove(applicationId);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to remove application.";
            }
            return response;
        }

        public Response<Application> Load(int applicationId)
        {
            var response = new Response<Application>();
            try
            {
                response.Success = true;
                response.Message = "Loaded application.";
                response.Data = _repo.Load(applicationId);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to load application.";
            }
            return response;
        }

        public Response<List<Application>> LoadAll()
        {
            var response = new Response<List<Application>>();
            try
            {
                response.Success = true;
                response.Message = "Loaded all applications.";
                response.Data = _repo.LoadAll().OrderBy(a => a.LastName).ToList();
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to load all applications.";
            }
            return response;
        }
    }
}