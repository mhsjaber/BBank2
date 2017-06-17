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
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Area { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
        public DateTime CreatedOn { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string BloodGroup { get; set; }
        public AccountStatus Status { get; set; }
        [Required, NotMapped]
        public string RePassword { get; set; }
    }
}