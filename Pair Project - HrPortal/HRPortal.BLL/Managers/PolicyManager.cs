using System;
using System.Collections.Generic;
using System.Linq;
using HRPortal.Contracts.Manager;
using HRPortal.Contracts.Repository;
using HRPortal.Data;
using HRPortal.Models;

namespace HRPortal.BLL.Managers
{
    public class PolicyManager : IPolicyManager
    {
        private readonly IPolicyRepository _repo;

        public PolicyManager()
        {
            _repo = RepositoryFactory.GetPolicyRepository();
        }

        public PolicyManager(bool nUnitTest)
        {
            _repo = RepositoryFactory.GetPolicyRepository(nUnitTest);
        }

        public Response<Policy> Add(Policy policyToAdd)
        {
            var response = new Response<Policy>();
            try
            {
                response.Success = true;
                response.Message = "Added policy.";
                response.Data = _repo.Add(policyToAdd);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to add policy.";
            }
            return response;
        }

        public Response<Policy> Edit(int policyId, Policy policyToEdit)
        {
            var response = new Response<Policy>();
            try
            {
                response.Success = true;
                response.Message = "Edited policy.";
                response.Data = _repo.Edit(policyId, policyToEdit);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to edit policy.";
            }
            return response;
        }

        public Response<int> Remove(int policyId)
        {
            var response = new Response<int>();
            try
            {
                response.Success = true;
                response.Message = "Removed policy.";
                response.Data = _repo.Remove(policyId);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to remove policy.";
            }
            return response;
        }

        public Response<Policy> Load(int policyId)
        {
            var response = new Response<Policy>();
            try
            {
                response.Success = true;
                response.Message = "Loaded policy.";
                response.Data = _repo.Load(policyId);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to load policy.";
            }
            return response;
        }

        public Response<List<Policy>> LoadAll()
        {
            var response = new Response<List<Policy>>();
            try
            {
                response.Success = true;
                response.Message = "Loaded all policies.";
                response.Data = _repo.LoadAll().OrderBy(p => p.Title).ToList();
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to load all policies.";
            }
            return response;
        }
    }
}