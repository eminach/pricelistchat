using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace PriceList.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<PriceList.Models.Connection> Connections { get; set; }

        public System.Data.Entity.DbSet<PriceList.Models.Brand> Brands { get; set; }

        public System.Data.Entity.DbSet<PriceList.Models.Model> Models { get; set; }

        public System.Data.Entity.DbSet<PriceList.Models.Device> Devices { get; set; }

        public System.Data.Entity.DbSet<PriceList.Models.Message> Messages { get; set; }

        public System.Data.Entity.DbSet<PriceList.Models.Reply> Replies { get; set; }

        public System.Data.Entity.DbSet<PriceList.Models.UserType> UserTypes { get; set; }

        public System.Data.Entity.DbSet<PriceList.Models.CompanyPriceList> PriceLists { get; set; }
    }
}
