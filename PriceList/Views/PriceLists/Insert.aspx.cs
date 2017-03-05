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
    public partial class Insert : System.Web.UI.Page
    {
		protected PriceList.Models.ApplicationDbContext _db = new PriceList.Models.ApplicationDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        // This is the Insert method to insert the entered CompanyPriceList item
        // USAGE: <asp:FormView InsertMethod="InsertItem">
        public void InsertItem()
        {
            using (_db)
            {
                var item = new PriceList.Models.CompanyPriceList();

                TryUpdateModel(item);

                if (ModelState.IsValid)
                {
                    // Save changes
                    _db.PriceLists.Add(item);
                    _db.SaveChanges();

                    Response.Redirect("Default");
                }
            }
        }

        protected void ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Cancel", StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("Default");
            }
        }
    }
}
