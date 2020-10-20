using BAI.Adir.Api.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace BAI.Adir.Api.Domain.Context
{
    public class AdirContext : DbContext
    {
        public AdirContext() : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();



        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Barangay> Barangays { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<DiseaseIncident> DiseaseIncidents { get; set; }


        public DbSet<ContentFile> ContentFiles { get; set; }

        public DbSet<ContentFileType> ContentFileTypes { get; set; }

        public DbSet<DiseaseIncidentFile> DiseaseIncidentFiles { get; set; }

    }
}