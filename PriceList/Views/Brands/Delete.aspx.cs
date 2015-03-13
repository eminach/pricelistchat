using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using Microsoft.AspNet.FriendlyUrls.ModelBinding;
using PriceList.Models;

namespace PriceList.Views.Brands
{
    public partial class Delete : System.Web.UI.Page
    {
		protected PriceList.Models.ApplicationDbContext _db = new PriceList.Models.ApplicationDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // This is the Delete methd to delete the selected Brand item
        // USAGE: <asp:FormView DeleteMethod="DeleteItem">
        public void DeleteItem(int ID)
        {
            using (_db)
            {
                var item = _db.Brands.Find(ID);

                if (item != null)
                {
                    _db.Brands.Remove(item);
                    _db.SaveChanges();
                }
            }
            Response.Redirect("../Default");
        }

        // This is the Select methd to selects a single Brand item with the id
        // USAGE: <asp:FormView SelectMethod="GetItem">
        public PriceList.Models.Brand GetItem([FriendlyUrlSegmentsAttribute(0)]int? ID)
        {
            if (ID == null)
            {
                return null;
            }

            using (_db)
            {
	            return _db.Brands.Where(m => m.ID == ID).FirstOrDefault();
            }
        }

        protected void ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Cancel", StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("../Default");
            }
        }
    }
}

