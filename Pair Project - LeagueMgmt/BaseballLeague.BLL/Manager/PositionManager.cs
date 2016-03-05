using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseballLeague.CONTRACTS.Manager;
using BaseballLeague.CONTRACTS.Repository;
using BaseballLeague.DATA;
using BaseballLeague.DATA.Repository;
using BaseballLeague.MODELS;

namespace BaseballLeague.BLL.Manager
{
    public class PositionManager : IPositionManager
    {
        private readonly IPositionRepository _repo;

        public PositionManager()
        {
            _repo = RepositoryFactory.GetPositionRepository();
        }

        public Response<List<Position>> LoadAll()
        {
            var response = new Response<List<Position>>();
            try
            {
                response.Success = true;
                response.Message = "Positions loaded.";
                response.Data = _repo.LoadAll();
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to load Positions.";
                response.Data = new List<Position>();
            }
            return response;
        }
    }
}
