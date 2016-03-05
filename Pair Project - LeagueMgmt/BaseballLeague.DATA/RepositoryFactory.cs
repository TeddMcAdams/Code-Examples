using BaseballLeague.CONTRACTS.Repository;
using BaseballLeague.DATA.Repository;

namespace BaseballLeague.DATA
{
    public static class RepositoryFactory
    {
        public static IPlayerRepository GetPlayerRepository()
        {
            return new PlayerRepository();
        }

        public static ITeamMgrRepository GetManagerRepository()
        {
            return new TeamMgrRepository();
        }

        public static ITeamRepository GetTeamRepository()
        {
            return new TeamRepository();
        }

        public static IPositionRepository GetPositionRepository()
        {
            return new PositionRepository();
        }
    }
}