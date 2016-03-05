using System.Collections.Generic;
using HRPortal.Models;

namespace HRPortal.Contracts.Repository
{
    public interface ITimeSheetRepository
    {
        TimeSheet Add(TimeSheet timeSheetToAdd);
        TimeSheet Edit(int timeSheetId, TimeSheet timeSheetToEdit);
        int Remove(int timeSheetId);
        TimeSheet Load(int timeSheetId);
        List<TimeSheet> LoadAll();
    }
}