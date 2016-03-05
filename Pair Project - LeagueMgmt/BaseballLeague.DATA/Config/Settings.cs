using System.Configuration;

namespace BaseballLeague.DATA.Config
{
    public static class Settings
    {
        public static string ConnectionString { get; private set; }

        static Settings()
        {
            ConnectionString = ConfigurationManager.AppSettings["Mode"] == "Production"
                ? ConfigurationManager.ConnectionStrings["BaseballLeague"].ConnectionString
                : ConfigurationManager.ConnectionStrings["TestBaseballLeague"].ConnectionString;
        }
    }
}