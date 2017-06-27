using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BloodBank.Models.EntityDiagram
{
    public class Admin
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public string OldPassword { get; set; }
        [NotMapped]
        public string RePassword { get; set; }
    }
}