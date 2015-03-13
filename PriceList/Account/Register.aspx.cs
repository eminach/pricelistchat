using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using PriceList.Models;
using System.Data.Entity.Validation;

namespace PriceList.Account
{
    public partial class Register : Page
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                drpType.DataSource = db.UserTypes.ToList();
                drpType.DataBind();
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var context = Context.GetOwinContext().Get<ApplicationDbContext>();
            Guid typeID = Guid.Parse(drpType.SelectedValue);
            var type = context.UserTypes.First(t => t.ID == typeID);
            var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text, FirstName = FirstName.Text, CompanyName = CompanyName.Text, Contacts = Contacts.Text, Type = type };
           //code-a baxmisam RUN edek aha OK
            IdentityResult result = manager.Create(user, Password.Text);
            if (result.Succeeded)
            {
                string code = manager.GenerateEmailConfirmationToken(user.Id);
                string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                manager.SendEmail(user.Id, "Confirm your account", "Salam aleykum, Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                
            }
            else
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}