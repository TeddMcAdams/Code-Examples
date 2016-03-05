using System.Collections.Generic;
using HRPortal.Models;

namespace HRPortal.Contracts.Repository
{
    public interface IPolicyRepository
    {
        Policy Add(Policy policyToAdd);
        Policy Edit(int policyId, Policy policyToEdit);
        int Remove(int policyId);
        Policy Load(int policyId);
        List<Policy> LoadAll();
    }
}