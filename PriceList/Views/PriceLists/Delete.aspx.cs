﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using Microsoft.AspNet.FriendlyUrls.ModelBinding;
using PriceList.Models;

namespace PriceList.Views.PriceLists
{
    public partial class Delete : System.Web.UI.Page
    {
		protected PriceList.Models.ApplicationDbContext _db = new PriceList.Models.ApplicationDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // This is the Delete methd to delete the selected CompanyPriceList item
        // USAGE: <asp:FormView DeleteMethod="DeleteItem">
        public void DeleteItem(int ID)
        {
            using (_db)
            {
                var item = _db.PriceLists.Find(ID);

                if (item != null)
                {
                    _db.PriceLists.Remove(item);
                    _db.SaveChanges();
                }
            }
            Response.Redirect("../Default");
        }

        // This is the Select methd to selects a single CompanyPriceList item with the id
        // USAGE: <asp:FormView SelectMethod="GetItem">
        public PriceList.Models.CompanyPriceList GetItem([FriendlyUrlSegmentsAttribute(0)]int? ID)
        {
            if (ID == null)
            {
                return null;
            }

            using (_db)
            {
	            return _db.PriceLists.Where(m => m.ID == ID).Include(m => m.Device).FirstOrDefault();
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
