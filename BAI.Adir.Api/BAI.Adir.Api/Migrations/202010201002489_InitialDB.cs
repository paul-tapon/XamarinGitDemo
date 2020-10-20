namespace BAI.Adir.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppUser",
                c => new
                    {
                        AppUserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        EmailConfirmed = c.Boolean(nullable: false),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        MiddleName = c.String(maxLength: 50),
                        LastLoggedIn = c.DateTime(),
                        AccessFailedCount = c.Int(nullable: false),
                        PasswordHash = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.AppUserId)
                .Index(t => t.Username, unique: true, name: "UsernameIndex")
                .Index(t => t.Email, unique: true, name: "EmailIndex");
            
            CreateTable(
                "dbo.Barangay",
                c => new
                    {
                        BarangayId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        MunicipalityId = c.Int(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByAppUserId = c.Int(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedByAppUserId = c.Int(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.BarangayId)
                .ForeignKey("dbo.AppUser", t => t.CreatedByAppUserId)
                .ForeignKey("dbo.AppUser", t => t.ModifiedByAppUserId)
                .ForeignKey("dbo.Municipality", t => t.MunicipalityId, cascadeDelete: true)
                .Index(t => t.MunicipalityId)
                .Index(t => t.CreatedByAppUserId)
                .Index(t => t.ModifiedByAppUserId);
            
            CreateTable(
                "dbo.Municipality",
                c => new
                    {
                        MunicipalityId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Abbreviation = c.String(nullable: false, maxLength: 100),
                        ZipCode = c.String(maxLength: 6),
                        ProvinceId = c.Int(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByAppUserId = c.Int(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedByAppUserId = c.Int(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.MunicipalityId)
                .ForeignKey("dbo.AppUser", t => t.CreatedByAppUserId)
                .ForeignKey("dbo.AppUser", t => t.ModifiedByAppUserId)
                .ForeignKey("dbo.Province", t => t.ProvinceId)
                .Index(t => t.ProvinceId)
                .Index(t => t.CreatedByAppUserId)
                .Index(t => t.ModifiedByAppUserId);
            
            CreateTable(
                "dbo.Province",
                c => new
                    {
                        ProvinceId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Abbreviation = c.String(nullable: false, maxLength: 100),
                        RegionId = c.Int(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByAppUserId = c.Int(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedByAppUserId = c.Int(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ProvinceId)
                .ForeignKey("dbo.AppUser", t => t.CreatedByAppUserId)
                .ForeignKey("dbo.AppUser", t => t.ModifiedByAppUserId)
                .ForeignKey("dbo.Region", t => t.RegionId)
                .Index(t => t.RegionId)
                .Index(t => t.CreatedByAppUserId)
                .Index(t => t.ModifiedByAppUserId);
            
            CreateTable(
                "dbo.Region",
                c => new
                    {
                        RegionId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Abbreviation = c.String(nullable: false, maxLength: 100),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByAppUserId = c.Int(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedByAppUserId = c.Int(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.RegionId)
                .ForeignKey("dbo.AppUser", t => t.CreatedByAppUserId)
                .ForeignKey("dbo.AppUser", t => t.ModifiedByAppUserId)
                .Index(t => t.Name, unique: true, name: "NameIndex")
                .Index(t => t.CreatedByAppUserId)
                .Index(t => t.ModifiedByAppUserId);
            
            CreateTable(
                "dbo.ContentFile",
                c => new
                    {
                        ContentFileId = c.Int(nullable: false, identity: true),
                        ContentFileTypeId = c.Int(nullable: false),
                        FileName = c.String(),
                        FileType = c.String(),
                        FileSize = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ContentFileId)
                .ForeignKey("dbo.ContentFileType", t => t.ContentFileTypeId, cascadeDelete: true)
                .Index(t => t.ContentFileTypeId);
            
            CreateTable(
                "dbo.ContentFileType",
                c => new
                    {
                        ContentFileTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Folder = c.String(),
                    })
                .PrimaryKey(t => t.ContentFileTypeId);
            
            CreateTable(
                "dbo.DiseaseIncidentFile",
                c => new
                    {
                        DiseaseIncidentFileId = c.Int(nullable: false, identity: true),
                        DiseaseIncidentId = c.Int(nullable: false),
                        ContentFileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DiseaseIncidentFileId)
                .ForeignKey("dbo.ContentFile", t => t.ContentFileId, cascadeDelete: true)
                .ForeignKey("dbo.DiseaseIncident", t => t.DiseaseIncidentId, cascadeDelete: true)
                .Index(t => t.DiseaseIncidentId)
                .Index(t => t.ContentFileId);
            
            CreateTable(
                "dbo.DiseaseIncident",
                c => new
                    {
                        DiseaseIncidentId = c.Int(nullable: false, identity: true),
                        ReportedByFirstname = c.String(),
                        ReportedByLastname = c.String(),
                        ReportedByMiddleName = c.String(),
                        ReportedBySuffixName = c.String(),
                        ContactNumber = c.String(),
                        ContactPerson = c.String(),
                        SymptomsStartDate = c.DateTime(),
                        DeceasedDate = c.DateTime(),
                        NumberOfInfected = c.Int(nullable: false),
                        NumberOfDeaths = c.Int(nullable: false),
                        Population = c.Int(nullable: false),
                        Symptoms = c.String(),
                        Notes = c.String(),
                        BarangayId = c.Int(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByAppUserId = c.Int(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedByAppUserId = c.Int(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.DiseaseIncidentId)
                .ForeignKey("dbo.Barangay", t => t.BarangayId, cascadeDelete: true)
                .ForeignKey("dbo.AppUser", t => t.CreatedByAppUserId)
                .ForeignKey("dbo.AppUser", t => t.ModifiedByAppUserId)
                .Index(t => t.BarangayId)
                .Index(t => t.CreatedByAppUserId)
                .Index(t => t.ModifiedByAppUserId);
            
            CreateTable(
                "dbo.Species",
                c => new
                    {
                        SpeciesId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByAppUserId = c.Int(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedByAppUserId = c.Int(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.SpeciesId)
                .ForeignKey("dbo.AppUser", t => t.CreatedByAppUserId)
                .ForeignKey("dbo.AppUser", t => t.ModifiedByAppUserId)
                .Index(t => t.CreatedByAppUserId)
                .Index(t => t.ModifiedByAppUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Species", "ModifiedByAppUserId", "dbo.AppUser");
            DropForeignKey("dbo.Species", "CreatedByAppUserId", "dbo.AppUser");
            DropForeignKey("dbo.DiseaseIncidentFile", "DiseaseIncidentId", "dbo.DiseaseIncident");
            DropForeignKey("dbo.DiseaseIncident", "ModifiedByAppUserId", "dbo.AppUser");
            DropForeignKey("dbo.DiseaseIncident", "CreatedByAppUserId", "dbo.AppUser");
            DropForeignKey("dbo.DiseaseIncident", "BarangayId", "dbo.Barangay");
            DropForeignKey("dbo.DiseaseIncidentFile", "ContentFileId", "dbo.ContentFile");
            DropForeignKey("dbo.ContentFile", "ContentFileTypeId", "dbo.ContentFileType");
            DropForeignKey("dbo.Barangay", "MunicipalityId", "dbo.Municipality");
            DropForeignKey("dbo.Municipality", "ProvinceId", "dbo.Province");
            DropForeignKey("dbo.Province", "RegionId", "dbo.Region");
            DropForeignKey("dbo.Region", "ModifiedByAppUserId", "dbo.AppUser");
            DropForeignKey("dbo.Region", "CreatedByAppUserId", "dbo.AppUser");
            DropForeignKey("dbo.Province", "ModifiedByAppUserId", "dbo.AppUser");
            DropForeignKey("dbo.Province", "CreatedByAppUserId", "dbo.AppUser");
            DropForeignKey("dbo.Municipality", "ModifiedByAppUserId", "dbo.AppUser");
            DropForeignKey("dbo.Municipality", "CreatedByAppUserId", "dbo.AppUser");
            DropForeignKey("dbo.Barangay", "ModifiedByAppUserId", "dbo.AppUser");
            DropForeignKey("dbo.Barangay", "CreatedByAppUserId", "dbo.AppUser");
            DropIndex("dbo.Species", new[] { "ModifiedByAppUserId" });
            DropIndex("dbo.Species", new[] { "CreatedByAppUserId" });
            DropIndex("dbo.DiseaseIncident", new[] { "ModifiedByAppUserId" });
            DropIndex("dbo.DiseaseIncident", new[] { "CreatedByAppUserId" });
            DropIndex("dbo.DiseaseIncident", new[] { "BarangayId" });
            DropIndex("dbo.DiseaseIncidentFile", new[] { "ContentFileId" });
            DropIndex("dbo.DiseaseIncidentFile", new[] { "DiseaseIncidentId" });
            DropIndex("dbo.ContentFile", new[] { "ContentFileTypeId" });
            DropIndex("dbo.Region", new[] { "ModifiedByAppUserId" });
            DropIndex("dbo.Region", new[] { "CreatedByAppUserId" });
            DropIndex("dbo.Region", "NameIndex");
            DropIndex("dbo.Province", new[] { "ModifiedByAppUserId" });
            DropIndex("dbo.Province", new[] { "CreatedByAppUserId" });
            DropIndex("dbo.Province", new[] { "RegionId" });
            DropIndex("dbo.Municipality", new[] { "ModifiedByAppUserId" });
            DropIndex("dbo.Municipality", new[] { "CreatedByAppUserId" });
            DropIndex("dbo.Municipality", new[] { "ProvinceId" });
            DropIndex("dbo.Barangay", new[] { "ModifiedByAppUserId" });
            DropIndex("dbo.Barangay", new[] { "CreatedByAppUserId" });
            DropIndex("dbo.Barangay", new[] { "MunicipalityId" });
            DropIndex("dbo.AppUser", "EmailIndex");
            DropIndex("dbo.AppUser", "UsernameIndex");
            DropTable("dbo.Species");
            DropTable("dbo.DiseaseIncident");
            DropTable("dbo.DiseaseIncidentFile");
            DropTable("dbo.ContentFileType");
            DropTable("dbo.ContentFile");
            DropTable("dbo.Region");
            DropTable("dbo.Province");
            DropTable("dbo.Municipality");
            DropTable("dbo.Barangay");
            DropTable("dbo.AppUser");
        }
    }
}
