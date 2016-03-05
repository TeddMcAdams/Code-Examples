using System.Collections.Generic;
using HRPortal.Models;

namespace HRPortal.Contracts.Manager
{
    public interface IPolicyManager
    {
        Response<Policy> Add(Policy policyToAdd);
        Response<Policy> Edit(int policyId, Policy policyToEdit);
        Response<int> Remove(int policyId);
        Response<Policy> Load(int policyId);
        Response<List<Policy>> LoadAll();
    }
}