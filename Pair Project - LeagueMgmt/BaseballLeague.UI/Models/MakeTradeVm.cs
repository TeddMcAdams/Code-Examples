using BaseballLeague.MODELS;

namespace BaseballLeague.UI.Models
{
    public class MakeTradeVm
    {
        public Player Player { get; set; }
        public Team PlayersTeam { get; set; }
        public Player TradedForPlayer { get; set; }
        public Team TradedTeam { get; set; }

        public MakeTradeVm()
        {
            Player = new Player();
            PlayersTeam = new Team();
            TradedForPlayer = new Player();
            TradedTeam = new Team();
        }
    }
}