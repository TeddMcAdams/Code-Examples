using System;
using System.Collections.Generic;
using BaseballLeague.CONTRACTS.Manager;
using BaseballLeague.CONTRACTS.Repository;
using BaseballLeague.DATA;
using BaseballLeague.MODELS;

namespace BaseballLeague.BLL.Manager
{
    public class TeamManager : ITeamManager
    {
        private readonly ITeamRepository _repo;

        public TeamManager()
        {
            _repo = RepositoryFactory.GetTeamRepository();
        }

        public Response<List<Team>> LoadAll()
        {
            var response = new Response<List<Team>>();
            try
            {
                response.Success = true;
                response.Message = "Loaded teams.";
                response.Data = _repo.LoadAll();
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "An error occurred, please try again later.";
                response.Data = new List<Team>();
            }
            return response;
        }

        public Response<Team> Load(int teamId)
        {
            var response = new Response<Team>();

            try
            {
                response.Success = true;
                response.Message = "Team loaded.";
                response.Data = _repo.Load(teamId);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "An error occurred, please try again later.";
                response.Data = new Team();
            }
            return response;
        }

        public Response<Team> Add(Team teamToAdd)
        {
            var response = new Response<Team>();
            try
            {
                response.Success = true;
                response.Message = "Team accepted.";
                response.Data = _repo.Add(teamToAdd);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "An error occurred, please try again later.";
                response.Data = new Team();
            }
            return response;
        }

        public Response<Team> Edit(int teamId, Team teamToEdit)
        {
            var response = new Response<Team>();
            try
            {
                response.Success = true;
                response.Message = "Edited team";
                response.Data = _repo.Edit(teamId, teamToEdit);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "An error occurred, please try again later.";
                response.Data = new Team();
            }
            return response;
        }

        public Response<int> Remove(int teamId)
        {
            var response = new Response<int>();
            try
            {
                response.Success = true;
                response.Message = "Team removed.";
                response.Data = _repo.Remove(teamId);
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
