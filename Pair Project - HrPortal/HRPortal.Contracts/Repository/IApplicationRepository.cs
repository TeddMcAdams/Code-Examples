using System.Collections.Generic;
using HRPortal.Models;

namespace HRPortal.Contracts.Repository
{
    public interface IApplicationRepository
    {
        Application Add(Application applicationToAdd);
        Application Edit(int applicationId, Application applicationToEdit);
        int Remove(int applicationId);
        Application Load(int applicationId);
        List<Application> LoadAll();
    }
}