using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Milestone2.Models
{
    public class BikeDBContext : DbContext
    {
        public BikeDBContext() : base("Adventure")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<Milestone2.Models.BikeListModel> BikeListModels { get; set; }
    }
}
