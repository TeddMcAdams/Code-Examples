using System.Collections.Generic;
using HRPortal.Models;

namespace HRPortal.Contracts.Manager
{
    public interface ITimeSheetManager
    {
        Response<TimeSheet> Add(TimeSheet timeSheetToAdd);
        Response<TimeSheet> Edit(int timeSheetId, TimeSheet timeSheetToEdit);
        Response<int> Remove(int timeSheetId);
        Response<TimeSheet> Load(int timeSheetId);
        Response<List<TimeSheet>> LoadAll();
    }
}