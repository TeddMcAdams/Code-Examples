using System.Collections.Generic;
using System.Web.Mvc;
using BaseballLeague.MODELS;

namespace BaseballLeague.UI.Models
{
    public class TradePlayerVm
    {
        public Player Player { get; set; }
        public Team PlayersCurrTeam { get; set; }
        public Team TradeToTeam { get; set; }
        public int PlayerToTradeForId { get; set; }
        public IEnumerable<SelectListItem> PlayerSelectListItems => new SelectList(TradeToTeam.Players, "PlayerId", "FullName");
    }
}