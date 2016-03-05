using System;
using System.Collections.Generic;
using BaseballLeague.CONTRACTS.Manager;
using BaseballLeague.CONTRACTS.Repository;
using BaseballLeague.DATA;
using BaseballLeague.MODELS;

namespace BaseballLeague.BLL.Manager
{
    public class PlayerManager : IPlayerManager
    {
        private readonly IPlayerRepository _repo;

        public PlayerManager()
        {
            _repo = RepositoryFactory.GetPlayerRepository();
        }

        public Response<Player> Add(Player playerToAdd)
        {
            var response = new Response<Player>();
            try
            {
                response.Success = true;
                response.Message = "The player was accepted.";
                response.Data = _repo.Add(playerToAdd);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "The player could not be processed.";
                response.Data = new Player();
            }
            return response;
        }

        public Response<Player> Edit(int playerId, Player playerToEdit)
        {
            var response = new Response<Player>();
            try
            {
                response.Success = true;
                response.Message = "The player was edited successfully.";
                response.Data = _repo.Edit(playerId, playerToEdit);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to edit player.";
                response.Data = new Player();
            }
            return response;
        }

        public Response<Player> Load(int playerId)
        {
            var response = new Response<Player>();
            try
            {
                response.Success = true;
                response.Message = "Player loaded.";
                response.Data = _repo.Load(playerId);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Player failed to load.";
                response.Data = new Player();
            }
            return response;
        }

        public Response<List<Player>> LoadAll()
        {
            var response = new Response<List<Player>>();
            try
            {
                response.Success = true;
                response.Message = "Players loaded.";
                response.Data = _repo.LoadAll();
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to load Players.";
                response.Data = new List<Player>();
            }
            return response;
        }

        public Response<int> Remove(int playerId)
        {
            var response = new Response<int>();
            try
            {
                response.Success = true;
                response.Message = "Player removed.";
                response.Data = _repo.Remove(playerId);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to remove player.";
                response.Data = 0;
            }
            return response;
        }
    }
}
