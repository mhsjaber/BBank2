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
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string District { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        [Display(Name = "Create Date")]
        public DateTime CreatedOn { get; set; }
        [Display(Name = "Birth Date")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }
        public AccountStatus Status { get; set; }
        [NotMapped]
        public string RePassword { get; set; }
        [Display(Name = "Donation Status")]
        public string DonationStatus { get; set; }
        public DateTime? LastDonate { get; set; }
    }
}