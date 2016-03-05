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
    public class TeamRepository : ITeamRepository
    {
        private readonly string _cnStr;

        public TeamRepository()
        {
            _cnStr = Settings.ConnectionString;
        }

        public List<Team> LoadAll()
        {
            List<Team> teams;
            using (var cn = new SqlConnection(_cnStr))
            {
                teams = cn.Query<Team>("LoadAllTeams", commandType: CommandType.StoredProcedure).ToList();

                foreach (Team t in teams)
                {
                    var p = new DynamicParameters();
                    p.Add("TeamId", t.TeamId);
                    t.Manager = cn.Query<TeamMgr>("LoadManagerForTeam", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    t.Players = cn.Query<Player>("LoadPlayersForTeam", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }

            return teams;
        }

        public Team Load(int teamId)
        {
            Team team;
            using (var cn = new SqlConnection(_cnStr))
            {
                var p = new DynamicParameters();
                p.Add("TeamId", teamId);
                team = cn.Query<Team>("LoadTeamById", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                
                if (team == null) return null;
                team.Manager =
                    cn.Query<TeamMgr>("LoadManagerForTeam", p, commandType: CommandType.StoredProcedure)
                        .FirstOrDefault();

                team.Players = cn.Query<Player>("LoadPlayersForTeam", p, commandType: CommandType.StoredProcedure).ToList();

                var p2 = new DynamicParameters();
                foreach (Player player in team.Players)
                {
                    p2.Add("PlayerId", player.PlayerId);
                    player.Position =
                        cn.Query<Position>("LoadPositionForPlayer", p2, commandType: CommandType.StoredProcedure)
                            .FirstOrDefault();
                }
            }
            return team;
        }

        public Team Add(Team teamToAdd)
        {
            Team team;

            using (var cn = new SqlConnection(_cnStr))
            {
                var p = new DynamicParameters();
                p.Add("LeagueId", teamToAdd.LeagueId);
                p.Add("ManagerId", teamToAdd.Manager.ManagerId);
                p.Add("TeamName", teamToAdd.Name);

                team = cn.Query<Team>("AddTeam", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return team;
        }

        public Team Edit(int teamId, Team teamToEdit)
        {
            teamToEdit.TeamId = teamId;

            using (var cn = new SqlConnection(_cnStr))
            {
                var p = new DynamicParameters();
                p.Add("TeamId", teamToEdit.TeamId);
                p.Add("LeagueId", teamToEdit.LeagueId);
                p.Add("ManagerId", teamToEdit.Manager.ManagerId);
                p.Add("TeamName", teamToEdit.Name);

                cn.Query("EditTeam", p, commandType: CommandType.StoredProcedure);
            }
            return Load(teamId);
        }
        
        public int Remove(int teamId)
        {
            using (var cn = new SqlConnection(_cnStr))
            {
                var p = new DynamicParameters();
                p.Add("TeamId", teamId);

                cn.Query("RemoveTeam", p, commandType: CommandType.StoredProcedure);
            }
            return teamId;
        }
    }
}
