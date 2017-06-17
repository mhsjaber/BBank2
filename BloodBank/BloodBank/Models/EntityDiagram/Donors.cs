using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BloodBank.Models.EntityDiagram
{
    public class Donor
    {
        public Guid ID { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string District { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string BloodGroup { get; set; }
        public AccountStatus Status { get; set; }
        [NotMapped]
        public string RePassword { get; set; }
        public string DontationStatus { get; set; }
    }
}