using System.Collections.Generic;

namespace BaseballLeague.MODELS
{
    public class League
    {
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public List<Team> Teams { get; set; }
    }
}