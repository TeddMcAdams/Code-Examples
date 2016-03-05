using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BaseballLeague.MODELS
{
    public class Player : IValidatableObject
    {
        public int PlayerId { get; set; }

        public int TeamId { get; set; }
        public int PositionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? JerseyNumber { get; set; }
        public Position Position { get; set; }
        public int? RookieYear { get; set; }

        public decimal? LastSeasonBatAvg { get; set; }

        public string BatAvgStr => LastSeasonBatAvg == null ? "n/a" : LastSeasonBatAvg.ToString();
        public string FullName => $"{LastName}, {FirstName}";

        public Player()
        {
            Position = new Position();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (Position.PositionId == 0)
            {
                errors.Add(new ValidationResult("Please select a position.", new[] { "Position.PositionId" }));
            }

            if (TeamId == 0)
            {
                errors.Add(new ValidationResult("Please select a team.", new[] { "TeamId" }));
            }

            if (string.IsNullOrWhiteSpace(FirstName))
            {
                errors.Add(new ValidationResult("Please enter player's first name.", new[] { "FirstName" }));
            }
            if (FirstName?.Length > 50)
            {
                errors.Add(new ValidationResult("First name cannot be more than 50 characters.", new[] { "FirstName" }));
            }
            if (string.IsNullOrWhiteSpace(LastName))
            {
                errors.Add(new ValidationResult("Please enter player's last name.", new[] { "LastName" }));
            }
            if (LastName?.Length > 50)
            {
                errors.Add(new ValidationResult("Last name cannot be more than 50 characters.", new[] { "LastName" }));
            }

            if (JerseyNumber < 0 || JerseyNumber > 99 || JerseyNumber == null)
            {
                errors.Add(new ValidationResult("Jersey number must be between 0 and 99.", new[] { "JerseyNumber" }));
            }

            if (RookieYear == null)
            {
                errors.Add(new ValidationResult("Please enter player's rookie year.", new[] { "RookieYear" }));
            }

            if (RookieYear > DateTime.Now.Year)
            {
                errors.Add(new ValidationResult("Rookie year cannot be in the future.", new[] { "RookieYear" }));
            }

            if (RookieYear < DateTime.Now.Year - 50)
            {
                errors.Add(new ValidationResult("Player cannot be in the league for more than 50 years.", new[] { "RookieYear" }));
            }

            if (LastSeasonBatAvg < 0 || LastSeasonBatAvg > 0.999M)
            {
                errors.Add(new ValidationResult("Batting average must be between 0 and 1.", new[] {"LastSeasonBatAvg"}));

            }

            return errors;
        }
    }
}