using System.Collections.Generic;
using BaseballLeague.MODELS;

namespace BaseballLeague.UI.Models
{
    public class ViewAllPlayersVm
    {
        public List<Player> Players { get; set; }
        public List<Team> Teams { get; set; }
    }
}