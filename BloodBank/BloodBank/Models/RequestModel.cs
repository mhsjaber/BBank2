using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BloodBank.Models.EntityDiagram;

namespace BloodBank.Models
{
    public class RequestModel
    {
        public Guid ID { get; set; }
        public string By { get; set; }
        public DateTime Date { get; set; }
        public Guid ById { get; set; }
        public Guid ToId { get; set; }
        public RequestStatus Status { get; set; }
        public string ToNum { get; set; }
        public string FromNum { get; set; }
    }
}