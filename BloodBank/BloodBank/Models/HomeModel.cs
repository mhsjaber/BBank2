using BloodBank.Models.EntityDiagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBank.Models
{
    public class HomeModel
    {
        public List<Event> Events { get; set; }
        public List<Donor> Donors { get; set; }
        public About About { get; set; }
    }
}