﻿using System;

namespace HRPortal.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime DateHired { get; set; }
    }
}