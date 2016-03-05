using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BaseballLeague.CONTRACTS.Repository;
using BaseballLeague.DATA.Config;
using BaseballLeague.MODELS;

namespace BaseballLeague.DATA.Repository
{
    public class TeamMgrRepository : ITeamMgrRepository
    {
        private readonly string _cnStr;

        public TeamMgrRepository()
        {
            _cnStr = Settings.ConnectionString;
        }

        private TeamMgr PopulateFromReader(SqlDataReader dr)
        {
            TeamMgr manager = new TeamMgr();

            manager.ManagerId = (int) dr["ManagerId"];
            manager.FirstName = dr["FirstName"].ToString();
            manager.LastName = dr["LastName"].ToString();
            return manager;
        }


        public List<TeamMgr> LoadAll()
        {
            List<TeamMgr> managers = new List<TeamMgr>();

            using (SqlConnection cn = new SqlConnection(_cnStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "GetManagers";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        managers.Add(PopulateFromReader(dr));
                    }
                }
            }
            return managers;
        }


        public TeamMgr Load(int managerId)
        {
            TeamMgr manager = new TeamMgr();

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "GetAManager";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ManagerId", managerId);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        manager = PopulateFromReader(dr);
                    }
                }
            }
            return manager;
        }


        public TeamMgr Add(TeamMgr managerToAdd)
        {
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "AddAManager";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@FirstName", managerToAdd.FirstName);
                cmd.Parameters.AddWithValue("@LastName", managerToAdd.LastName);
                SqlParameter outParam = new SqlParameter();
                outParam.ParameterName = "@ManagerId";
                outParam.SqlDbType = System.Data.SqlDbType.Int;
                outParam.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(outParam);
                

                cn.Open();
                cmd.ExecuteNonQuery();

                managerToAdd.ManagerId = (int)outParam.Value;
            }
            return managerToAdd;
        }

        public TeamMgr Edit(TeamMgr managerToEdit)
        {
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "EditAManager";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ManagerId", managerToEdit.ManagerId);
                cmd.Parameters.AddWithValue("@FirstName", managerToEdit.FirstName);
                cmd.Parameters.AddWithValue("@Lastname", managerToEdit.LastName);

                cn.Open();
                cmd.ExecuteNonQuery();

            }
            return managerToEdit;
        }

        public int Remove(int managerId)
        {
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DeleteAManager";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ManagerId", managerId);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
            return managerId;
        }
    }
}