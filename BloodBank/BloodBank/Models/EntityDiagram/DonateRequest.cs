using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBank.Models.EntityDiagram
{
    public enum RequestStatus
    {
        Requested,
        Accepted,
        Rejected
    }

    public class DonateRequest
    {
        public Guid ID { get; set; }
        public Guid RequestedBy { get; set; }
        public Guid RequestedTo { get; set; }
        public DateTime CreatedOn { get; set; }
        public RequestStatus Status { get; set; }
    }
}