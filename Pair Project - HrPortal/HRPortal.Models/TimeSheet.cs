using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRPortal.Models
{
    public class TimeSheet : IValidatableObject
    {
        public int TimeSheetId { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        public decimal HoursWorked { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            
            if (EmployeeId == 0)
            {
                errors.Add(new ValidationResult("Please select your name.", new[] { "EmployeeId" }));
            }

            if (HoursWorked <= 0 || HoursWorked > 16)
            {
                errors.Add(new ValidationResult("Hours entered must be between 0 and 16.", new[] { "HoursWorked" }));
            }
            return errors;
        }
    }
}