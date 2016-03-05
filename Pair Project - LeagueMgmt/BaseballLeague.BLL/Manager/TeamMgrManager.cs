using System;
using System.Collections.Generic;
using BaseballLeague.CONTRACTS.Manager;
using BaseballLeague.CONTRACTS.Repository;
using BaseballLeague.DATA;
using BaseballLeague.MODELS;

namespace BaseballLeague.BLL.Manager
{
    public class TeamMgrManager : ITeamMgrManager
    {
        private readonly ITeamMgrRepository _repo;

        public TeamMgrManager()
        {
            _repo = RepositoryFactory.GetManagerRepository();
        }

        public Response<TeamMgr> Add(TeamMgr managerToAdd)
        {
            var response = new Response<TeamMgr>();
            try
            {
                response.Success = true;
                response.Message = "The manager was accepted.";
                response.Data = _repo.Add(managerToAdd);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "An error occurred, please try again later.";
                response.Data = new TeamMgr();
            }
            return response;
        }

        public Response<TeamMgr> Edit(TeamMgr managerToEdit)
        {
            var response = new Response<TeamMgr>();
            try
            {
                response.Success = true;
                response.Message = "The manager was edited successfully.";
                response.Data = _repo.Edit(managerToEdit);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "An error occurred, please try again later.";
                response.Data = new TeamMgr();
            }
            return response;
        }

        public Response<TeamMgr> Load(int managerId)
        {
            var response = new Response<TeamMgr>();
            try
            {
                response.Success = true;
                response.Message = "Manager loaded.";
                response.Data = _repo.Load(managerId);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "An error occurred, please try again later.";
                response.Data = new TeamMgr();
            }
            return response;
        }

        public Response<List<TeamMgr>> LoadAll()
        {
            var response = new Response<List<TeamMgr>>();
            try
            {
                response.Success = true;
                response.Message = "Loaded all managers.";
                response.Data = _repo.LoadAll();
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "An error occurred, please try again later.";
                response.Data = new List<TeamMgr>();
            }
            return response;
        }

        public Response<int> Remove(int managerId)
        {
            var response = new Response<int>();
            try
            {
                response.Success = true;
                response.Message = "Manager removed.";
                response.Data = _repo.Remove(managerId);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "An error occurred, please try again later.";
                response.Data = 0;
            }
            return response;
        }
    }
}
