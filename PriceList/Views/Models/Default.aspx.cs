using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using PriceList.Models;

namespace PriceList.Views.Models
{
    public partial class Default : System.Web.UI.Page
    {
		protected PriceList.Models.ApplicationDbContext _db = new PriceList.Models.ApplicationDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Model binding method to get List of Model entries
        // USAGE: <asp:ListView SelectMethod="GetData">
        public IQueryable<PriceList.Models.Model> GetData()
        {
            return _db.Models.Include(m => m.Brand);
        }
    }
}

