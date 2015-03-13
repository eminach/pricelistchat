using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using PriceList.Models;

namespace PriceList.Views.Devices
{
    public partial class Insert : System.Web.UI.Page
    {
		protected PriceList.Models.ApplicationDbContext _db = new PriceList.Models.ApplicationDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // This is the Insert method to insert the entered Device item
        // USAGE: <asp:FormView InsertMethod="InsertItem">
        public void InsertItem()
        {
            using (_db)
            {
                var item = new PriceList.Models.Device();

                TryUpdateModel(item);
                ModelState.Remove("Model");
                if (ModelState.IsValid)
                {
                    // Save changes
                    item.Model = _db.Models.Find(item.ModelID);
                    item.Fullname = string.Format("{0} {1} {2}", item.Model.Brand.BrandName, item.Model.ModelName, item.Specification);
                    _db.Devices.Add(item);
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
