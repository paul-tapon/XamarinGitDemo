namespace BAI.Adir.Api.Migrations
{
    using BAI.Adir.Api.Domain.Context;
    using BAI.Adir.Api.Domain.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BAI.Adir.Api.Domain.Context.AdirContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AdirContext context)
        {
            SeedContentFileType(context);
            SeedRegion(context);
            SeedProvince(context);
            SeedMunicipality(context);
            SeedBarangay(context);
        }

        private void SeedBarangay(AdirContext context)
        {
            var tarlacCity = context.Municipalities.FirstOrDefault(m => m.Name == "Tarlac city" && m.Province.Name =="Tarlac");

            var barangay1 = new Barangay()
            {
                Name = "Tibag",
                IsActive = true,
                CreatedOn = DateTime.Now,
                Municipality = tarlacCity
            };


            var barangay2 = new Barangay()
            {
                Name = "San Rafael",
                IsActive = true,
                CreatedOn = DateTime.Now,
                Municipality = tarlacCity
            };

            context.Barangays.AddOrUpdate(b => new { b.Name, b.MunicipalityId }, barangay1);
            context.Barangays.AddOrUpdate(b => new { b.Name, b.MunicipalityId }, barangay2);

            var gerona = context.Municipalities.FirstOrDefault(m => m.Name == "Gerona" && m.Province.Name == "Tarlac");

            var barangay3 = new Barangay()
            {
                Name = "Pinasling",
                IsActive = true,
                CreatedOn = DateTime.Now,
                Municipality = gerona
            };


            var barangay4 = new Barangay()
            {
                Name = "Poblacion 1",
                IsActive = true,
                CreatedOn = DateTime.Now,
                Municipality = gerona
            };
            context.Barangays.AddOrUpdate(b => new { b.Name, b.MunicipalityId }, barangay3);
            context.Barangays.AddOrUpdate(b => new { b.Name, b.MunicipalityId }, barangay4);
            context.SaveChanges();
        }

        private void SeedContentFileType(AdirContext context)
        {
            var welfare = new ContentFileType()
            {
                Name = "Welfare"
            };

            var disease = new ContentFileType()
            {
                Name = "Disease"
            };
            context.ContentFileTypes.AddOrUpdate(c => c.Name, welfare);
            context.ContentFileTypes.AddOrUpdate(c => c.Name, disease);

            context.SaveChanges();
        }

        private void SeedRegion(AdirContext context)
        {
            var region1 = new Region
            {
                Name = "Region 1",
                Abbreviation = "Reg. 1",
                CreatedOn = DateTime.Now,
                IsActive = true
            };

            var region2 = new Region
            {
                Name = "Region 2",
                Abbreviation = "Reg. 2",
                CreatedOn = DateTime.Now,
                IsActive = true
            };

            var region3 = new Region
            {
                Name = "Region 3",
                Abbreviation = "Reg. 3",
                CreatedOn = DateTime.Now,
                IsActive = true
            };

            context.Regions.AddOrUpdate(r => r.Name, region1);
            context.Regions.AddOrUpdate(r => r.Name, region2);
            context.Regions.AddOrUpdate(r => r.Name, region3);

            context.SaveChanges();
        }

        private void SeedProvince(AdirContext context)
        {
            var region1 = context.Regions.FirstOrDefault(r => r.Name == "Region 1");
            var ilocosNorte = new Province()
            {
                Name = "Ilocos Norte",
                Abbreviation = "IN",
                CreatedOn = DateTime.Now,
                IsActive = true,
                RegionId = region1.RegionId,
                Latitude=0,Longitude=0,
            };

            var ilocosSur = new Province()
            {
                Name = "Ilocos Sur",
                Abbreviation = "Is",
                CreatedOn = DateTime.Now,
                IsActive = true,
                RegionId = region1.RegionId,
                Latitude = 0,
                Longitude = 0


            };

            var laUnion = new Province()
            {
                Name = "La Union",
                Abbreviation = "LA",
                CreatedOn = DateTime.Now,
                IsActive = true,
                RegionId = region1.RegionId,
                Latitude = 0,
                Longitude = 0

            };

            context.Provinces.AddOrUpdate(r => r.Name, ilocosNorte);
            context.Provinces.AddOrUpdate(r => r.Name, ilocosSur);
            context.Provinces.AddOrUpdate(r => r.Name, laUnion);



            var region3 = context.Regions.FirstOrDefault(r => r.Name == "Region 3");
            var tarlac = new Province()
            {
                Name = "Tarlac",
                Abbreviation = "TR",
                CreatedOn = DateTime.Now,
                IsActive = true,
                RegionId = region3.RegionId,
                Latitude = 0,
                Longitude = 0

            };

            var pampanga = new Province()
            {
                Name = "Pampanga",
                Abbreviation = "PMP",
                CreatedOn = DateTime.Now,
                IsActive = true,
                RegionId = region3.RegionId,
                Latitude = 0,
                Longitude = 0

            };

            context.Provinces.AddOrUpdate(r => r.Name, tarlac);
            context.Provinces.AddOrUpdate(r => r.Name, pampanga);


            context.SaveChanges();
        }


        private void SeedMunicipality(AdirContext context)
        {
            var tarlacProvince = context.Provinces.FirstOrDefault(r => r.Name == "Tarlac");

            var gerona = new Municipality()
            {
                Name = "Gerona",
                Abbreviation = "Gerona",
                IsActive = true,
                CreatedOn = DateTime.Now,
                Province = tarlacProvince
            };

            var tarlacCity = new Municipality()
            {
                Name = "Tarlac City",
                Abbreviation = "Tarlac",
                IsActive = true,
                CreatedOn = DateTime.Now,
                Province = tarlacProvince
            };


            context.Municipalities.AddOrUpdate(m => m.Name, gerona);
            context.Municipalities.AddOrUpdate(m => m.Name, tarlacCity);

            context.SaveChanges();

        }

    }
}
