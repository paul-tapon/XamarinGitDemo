namespace BAI.Adir.Api.Migrations
{
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

        protected override void Seed(BAI.Adir.Api.Domain.Context.AdirContext context)
        {



            var welfare = new ContentFileType() 
            {
                Name ="Welfare"
            };

            var disease = new ContentFileType()
            {
                Name = "Disease"
            };
            context.ContentFileTypes.AddOrUpdate(c => c.Name, welfare);
            context.ContentFileTypes.AddOrUpdate(c => c.Name, disease);

            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
