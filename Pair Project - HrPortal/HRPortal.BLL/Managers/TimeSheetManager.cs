using System;
using System.Collections.Generic;
using System.Linq;
using HRPortal.Contracts.Manager;
using HRPortal.Contracts.Repository;
using HRPortal.Data;
using HRPortal.Models;

namespace HRPortal.BLL.Managers
{
    public class TimeSheetManager : ITimeSheetManager
    {
        private readonly ITimeSheetRepository _repo;

        public TimeSheetManager()
        {
            _repo = RepositoryFactory.GetTimeSheetRepository();
        }

        public TimeSheetManager(bool nUnitTest)
        {
            _repo = RepositoryFactory.GetTimeSheetRepository(nUnitTest);
        }

        public Response<TimeSheet> Add(TimeSheet timesheetToAdd)
        {
            var response = new Response<TimeSheet>();
            try
            {
                response.Success = true;
                response.Message = "Your time sheet was accepted, thank you.";
                response.Data = _repo.Add(timesheetToAdd);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Time sheet could not be processed. Please try again later.";
            }
            return response;
        }

        public Response<TimeSheet> Edit(int timeSheetId, TimeSheet timeSheetToEdit)
        {
            var response = new Response<TimeSheet>();
            try
            {
                response.Success = true;
                response.Message = "Edited time sheet.";
                response.Data = _repo.Edit(timeSheetId, timeSheetToEdit);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to edit time sheet.";
            }
            return response;
        }

        public Response<int> Remove(int timeSheetId)
        {
            var response = new Response<int>();
            try
            {
                response.Success = true;
                response.Message = "Removed time sheet.";
                response.Data = _repo.Remove(timeSheetId);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to remove time sheet.";
            }
            return response;
        }

        public Response<TimeSheet> Load(int timesheetId)
        {
            var response = new Response<TimeSheet>();
            try
            {
                response.Success = true;
                response.Message = "Time sheet loaded.";
                response.Data = _repo.Load(timesheetId);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Time sheet failed to load.";
            }
            return response;
        }

        public Response<List<TimeSheet>> LoadAll()
        {
            var response = new Response<List<TimeSheet>>();
            try
            {
                response.Success = true;
                response.Message = "Loaded all time sheets.";
                response.Data = _repo.LoadAll().OrderBy(t => t.TimeSheetId).ToList();
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to load all time sheets.";
            }
            return response;
        }
    }
}