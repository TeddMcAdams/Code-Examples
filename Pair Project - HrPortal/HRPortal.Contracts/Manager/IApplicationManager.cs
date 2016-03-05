using System.Collections.Generic;
using HRPortal.Models;

namespace HRPortal.Contracts.Manager
{
    public interface IApplicationManager
    {
        Response<Application> Add(Application applicationToAdd);
        Response<Application> Edit(int applicationId, Application applicationToEdit);
        Response<int> Remove(int applicationId);
        Response<Application> Load(int applicationId);
        Response<List<Application>> LoadAll();
    }
}