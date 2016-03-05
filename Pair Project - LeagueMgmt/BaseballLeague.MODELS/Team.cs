using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BaseballLeague.MODELS
{
    public class Team : IValidatableObject
    {
        public int TeamId { get; set; }
        public List<Player> Players { get; set; }

        public int LeagueId { get; set; }
        public string Name { get; set; }
        public TeamMgr Manager { get; set; }

        public Team()
        {
            Manager = new TeamMgr();
            Players = new List<Player>();
        }

        public Team(int leagueId)
        {
            LeagueId = leagueId;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(Name))
            {
                errors.Add(new ValidationResult("Please enter team name.", new[] {"Name"}));
            }

            if (string.IsNullOrWhiteSpace(Manager.FirstName))
            {
                errors.Add(new ValidationResult("Please enter Manager's first name.", new[] {"Manager.FirstName"}));
            }

            if (string.IsNullOrWhiteSpace(Manager.LastName))
            {
                errors.Add(new ValidationResult("Please enter Manager's last name.", new[] {"Manager.LastName"}));
            }

            return errors;
        }
    }
}