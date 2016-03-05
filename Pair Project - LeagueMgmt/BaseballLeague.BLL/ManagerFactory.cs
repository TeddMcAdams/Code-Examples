using BaseballLeague.BLL.Manager;
using BaseballLeague.CONTRACTS.Manager;

namespace BaseballLeague.BLL
{
    public static class ManagerFactory
    {
        public static ITeamManager GetTeamManager()
        {
            return new TeamManager();
        }

        public static ITeamMgrManager GetTeamMgrManager()
        {
            return new TeamMgrManager();
        }

        public static IPlayerManager GetPlayerManager()
        {
            return new PlayerManager();
        }

        public static IPositionManager GetPosition()
        {
            return new PositionManager();
        }
    }
}
