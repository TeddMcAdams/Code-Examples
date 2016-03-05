using System.Collections.Generic;
using BaseballLeague.BLL.Manager;
using BaseballLeague.MODELS;

namespace BaseballLeague.UI.Models
{
    public class CreatePlayerVm
    {
        public List<Team> AllTeams { get; set; }
        public List<Position> AllPositions { get; set; }
        public Player PlayerToAdd { get; set; }

        public CreatePlayerVm()
        {
            AllTeams = new List<Team>();
            AllPositions = new List<Position>();
            PlayerToAdd = new Player();
        }
    }
}