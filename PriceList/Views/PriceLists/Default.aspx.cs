using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using PriceList.Models;

namespace PriceList.Views.PriceLists
{
    public partial class Default : System.Web.UI.Page
    {
		protected PriceList.Models.ApplicationDbContext _db = new PriceList.Models.ApplicationDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Model binding method to get List of CompanyPriceList entries
        // USAGE: <asp:ListView SelectMethod="GetData">
        public IQueryable<PriceList.Models.CompanyPriceList> GetData()
        {
            return _db.PriceLists.Include(m => m.Device);
        }
    }
}

