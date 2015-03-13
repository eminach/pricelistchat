using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using PriceList.Models;

namespace PriceList
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using(var context = new ApplicationDbContext())
            {
                //context.Brands.AddRange(new Brand[] {
                //    new Brand() { BrandID = 1, BrandName = "Samsung", Description = "", LogoPath = "" },
                //    new Brand() { BrandID = 2, BrandName = "Apple", Description = "", LogoPath = "" },
                //    new Brand() { BrandID = 3, BrandName = "HTC", Description = "", LogoPath = "" }
                //});

                //context.Models.Add(new Model() { ModelID = 1, BrandID = 1, ModelName = "S5 mini duos" });
               // context.SaveChanges();
            }
        }
    }
}