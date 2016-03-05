using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BaseballLeague.CONTRACTS.Repository;
using BaseballLeague.DATA.Config;
using BaseballLeague.MODELS;

namespace BaseballLeague.DATA.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly string _cnStr;

        public PlayerRepository()
        {
            _cnStr = Settings.ConnectionString;
        }

        public List<Player> LoadAll()
        {
            var players = new List<Player>();

            using (var cn = new SqlConnection(_cnStr))
            {
                var cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "LoadAllPlayers",
                    Connection = cn
                };

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        players.Add(PopulateFromDataReader(dr));
                    }
                }
            }
            return players;
        }

        public Player Load(int playerId)
        {
            var player = new Player();
            
            using (var cn = new SqlConnection(_cnStr))
            {
                var cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "LoadPlayerById",
                    Connection = cn
                };

                cmd.Parameters.AddWithValue("@PlayerId", playerId);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        player = PopulateFromDataReader(dr);
                    }
                }

            }
            return player;
        }

        public Player Add(Player playerToAdd)
        {
            var player = new Player();

            using (var cn = new SqlConnection(_cnStr))
            {
                var cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "AddPlayer",
                    Connection = cn
                };
                cmd.Parameters.AddWithValue("@TeamId", playerToAdd.TeamId);
                cmd.Parameters.AddWithValue("@PositionId", playerToAdd.Position.PositionId);
                cmd.Parameters.AddWithValue("@FirstName", playerToAdd.FirstName);
                cmd.Parameters.AddWithValue("@LastName", playerToAdd.LastName);
                cmd.Parameters.AddWithValue("@JerseyNumber", playerToAdd.JerseyNumber);
                cmd.Parameters.AddWithValue("@RookieYear", playerToAdd.RookieYear);
                cmd.Parameters.AddWithValue("@LastSeasonBatAvg", (object) playerToAdd.LastSeasonBatAvg ?? DBNull.Value);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        player = PopulateFromDataReader(dr);
                    }
                }
            }
            return player;
        }

        public Player Edit(int playerId, Player playerToEdit)
        {
            var player = new Player();

            playerToEdit.PlayerId = playerId;

            using (var cn = new SqlConnection(_cnStr))
            {
                var cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "EditPlayer",
                    Connection = cn
                };
                cmd.Parameters.AddWithValue("@PlayerId", playerToEdit.PlayerId);
                cmd.Parameters.AddWithValue("@TeamId", playerToEdit.TeamId);
                cmd.Parameters.AddWithValue("@PositionId", playerToEdit.Position.PositionId);
                cmd.Parameters.AddWithValue("@FirstName", playerToEdit.FirstName);
                cmd.Parameters.AddWithValue("@LastName", playerToEdit.LastName);
                cmd.Parameters.AddWithValue("@JerseyNumber", playerToEdit.JerseyNumber);
                cmd.Parameters.AddWithValue("@RookieYear", playerToEdit.RookieYear);
                cmd.Parameters.AddWithValue("@LastSeasonBatAvg", (object)playerToEdit.LastSeasonBatAvg ?? DBNull.Value);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        player = PopulateFromDataReader(dr);
                    }
                }
            }
            return player;
        }

        public int Remove(int playerId)
        {
            using (var cn = new SqlConnection(_cnStr))
            {
                var cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "RemovePlayer",
                    Connection = cn
                };
                cmd.Parameters.AddWithValue("@PlayerId", playerId);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            return playerId;
        }
         
        private Player PopulateFromDataReader(SqlDataReader dr)
        {
            var player = new Player()
            {
                PlayerId = (int)dr["PlayerId"],
                TeamId = (int)dr["TeamId"],
                FirstName = dr["FirstName"].ToString(),
                LastName = dr["LastName"].ToString(),
                JerseyNumber = (int)dr["JerseyNumber"],

                Position = new Position
                {
                    PositionId = (int)dr["PositionId"],
                    Name = dr["PositionName"].ToString(),
                    Abbreviation = dr["Abbreviation"].ToString()
                },
                RookieYear = (int)dr["RookieYear"]
            };
            if (dr["LastSeasonBatAvg"] == DBNull.Value) return player;

            player.LastSeasonBatAvg = (decimal) dr["LastSeasonBatAvg"];

            return player;
        }
    }
}