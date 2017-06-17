using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBank.Models.EntityDiagram
{
    public class Event
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string   Objective { get; set; }
        public DateTime EventDate { get; set; }
    }
}