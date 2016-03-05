using System.Data.SqlClient;
using System.IO;
using BaseballLeague.DATA.Config;

namespace BaseballLeague.TESTS
{
    internal static class Utilities
    {
        internal static void RebuildTestDb()
        {
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var cmd = new SqlCommand
                {
                    CommandText = new FileInfo("Scripts/RebuildTestDB.txt").OpenText().ReadToEnd(),
                    Connection = cn
                };

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}