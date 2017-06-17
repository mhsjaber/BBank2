using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BloodBank.Models.EntityDiagram
{
    public class DonorDBContext : DbContext
    {
        public DbSet<Donor> Donor { get; set; }
        public DbSet<ContactMessage> ContactMessage { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<Event> Event { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}