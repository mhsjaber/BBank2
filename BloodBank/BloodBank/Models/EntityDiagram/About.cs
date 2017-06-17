using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBank.Models.EntityDiagram
{
    public class About
    {
        public Guid ID { get; set; }
        public string Mission { get; set; }
        public string Vision { get; set; }
        public string CharitableWorks { get; set; }
    }
}