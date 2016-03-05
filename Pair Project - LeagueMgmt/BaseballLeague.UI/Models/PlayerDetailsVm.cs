using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BaseballLeague.MODELS;

namespace BaseballLeague.UI.Models
{
    public class PlayerDetailsVm
    {
        public Player Player { get; set; }
        public List<Team> Teams { get; set; }
        public int TradeToTeamId { get; set; }
        public IEnumerable<SelectListItem> TeamSelectListItems => new SelectList(Teams.Where(t => t.TeamId != Player.TeamId), "TeamId", "Name");
    }
}