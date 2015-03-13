namespace PriceList.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using PriceList.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<PriceList.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "PriceList.Models.ApplicationDbContext";
        }

        protected override void Seed(PriceList.Models.ApplicationDbContext context)
        {
            context.UserTypes.AddOrUpdate<UserType>(x=>x.ID,
                new UserType(){ID=Guid.NewGuid(), Name="Retail"});
            context.Brands.AddOrUpdate<Brand>(x => x.ID,
                new Brand() { BrandName = "Samsung" },
                new Brand() { BrandName = "Apple" },
                new Brand() { BrandName = "HTC" },
                new Brand() { BrandName = "Sony" }
                );
            //context.Models.AddOrUpdate<Model>(m => m.ID,
            //    new Model() { ModelName = "Galaxy S5 Mini SM-G800H", Description="-", BrandID=2 },
            //    new Model() { ModelName = "Galaxy S5 SM-G900H", Description="-", BrandID = 2 },
            //    new Model() { ModelName = "Galaxy Alpha G850F", Description = "-", BrandID = 2 }
            //    );
            //context.Devices.AddOrUpdate<Device>(m => m.ID,
            //    new Device() { Specification = "3G 16Gb Charcoal Black", ModelID=7 },
            //    new Device() { Specification = "3G 16Gb Electric Blue", ModelID=8},
            //    new Device() { Specification = "32GB LTE Sleek Silver", ModelID=9 }
            //    );
        }
    }
}
