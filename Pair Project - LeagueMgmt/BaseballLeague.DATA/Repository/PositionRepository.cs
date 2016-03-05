using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BaseballLeague.CONTRACTS.Repository;
using BaseballLeague.DATA.Config;
using BaseballLeague.MODELS;
using Dapper;

namespace BaseballLeague.DATA.Repository
{
    public class PositionRepository : IPositionRepository
    {
        private readonly string _cnStr;

        public PositionRepository()
        {
            _cnStr = Settings.ConnectionString;
        }

        public List<Position> LoadAll()
        {
            List<Position> positions;

            using (var cn = new SqlConnection(_cnStr))
            {
                positions = cn.Query<Position>("LoadAllPositions", commandType: CommandType.StoredProcedure).ToList();
            }
            return positions;
        }

        private Position PopulateFromDataReader(SqlDataReader dr)
        {
            var position = new Position()
            {
                PositionId = (int) dr["PositionId"],
                Name = dr["Name"].ToString(),
                Abbreviation = dr["Abbreviation"].ToString()
            };
            return position;
        }
    }
}
