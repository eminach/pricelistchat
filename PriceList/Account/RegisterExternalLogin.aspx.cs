using System;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using PriceList.Models;
using System.Security.Claims;

namespace PriceList.Account
{
    public partial class RegisterExternalLogin : System.Web.UI.Page
    {
        protected string ProviderName
        {
            get { return (string)ViewState["ProviderName"] ?? String.Empty; }
            private set { ViewState["ProviderName"] = value; }
        }

        protected string ProviderAccountKey
        {
            get { return (string)ViewState["ProviderAccountKey"] ?? String.Empty; }
            private set { ViewState["ProviderAccountKey"] = value; }
        }

        private void RedirectOnFail()
        {
            Response.Redirect((User.Identity.IsAuthenticated) ? "~/Account/Manage" : "~/Account/Login");
        }

        protected void Page_Load()
        {
            // Process the result from an auth provider in the request
            ProviderName = IdentityHelper.GetProviderNameFromRequest(Request);
            
            if (String.IsNullOrEmpty(ProviderName))
            {
                RedirectOnFail();
                return;
            }
            if (!IsPostBack)
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
                var loginInfo = Context.GetOwinContext().Authentication.GetExternalLoginInfo();
                if (loginInfo == null)
                {
                    RedirectOnFail();
                    return;
                }
                var user = manager.Find(loginInfo.Login);
                if (user != null)
                {
                    signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
                else if (User.Identity.IsAuthenticated)
                {
                    // Apply Xsrf check when linking
                    var verifiedloginInfo = Context.GetOwinContext().Authentication.GetExternalLoginInfo(IdentityHelper.XsrfKey, User.Identity.GetUserId());
                    if (verifiedloginInfo == null)
                    {
                        RedirectOnFail();
                        return;
                    }

                    var result = manager.AddLogin(User.Identity.GetUserId(), verifiedloginInfo.Login);
                    if (result.Succeeded)
                    {
                        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                    }
                    else
                    {
                        AddErrors(result);
                        return;
                    }
                }
                else
                {
                    email.Text = loginInfo.Email;
                    
                }
            }
        }        
        
        protected void LogIn_Click(object sender, EventArgs e)
        {
            CreateAndLoginUser();
        }

        private void CreateAndLoginUser()
        {
            if (!IsValid)
            {
                return;
            }
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            var loginInfo = Context.GetOwinContext().Authentication.GetExternalLoginInfo();
            var user = new ApplicationUser() { };
            if (loginInfo.Login.LoginProvider == "Google")
            {
                var externalIdentity = Context.GetOwinContext().Authentication.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);
                var emailClaim = externalIdentity.Result.FindFirst(c => c.Type == ClaimTypes.Email);
                var lastNameClaim = externalIdentity.Result.FindFirst(c => c.Type == ClaimTypes.Surname);
                var givenNameClaim = externalIdentity.Result.FindFirst(c => c.Type == ClaimTypes.GivenName);

                user.Email= emailClaim.Value;
                user.FirstName= givenNameClaim.Value;
                user.UserName = lastNameClaim.Value;
                user.CompanyName = CompanyName.Text;
                user.Contacts = Contacts.Text;
            }
            else
            {
                user.FirstName = FirstName.Text;
                user.Email = email.Text;
                user.CompanyName = CompanyName.Text;
                user.Contacts = Contacts.Text;
            }
            
            IdentityResult result = manager.Create(user);
            if (result.Succeeded)
            {
                
                if (loginInfo == null)
                {
                    RedirectOnFail();
                    return;
                }
                
                result = manager.AddLogin(user.Id, loginInfo.Login);
                if (result.Succeeded)
                {
                    signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // var code = manager.GenerateEmailConfirmationToken(user.Id);
                    // Send this link via email: IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id)

                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                    return;
                }
            }
            AddErrors(result);
        }

        private void AddErrors(IdentityResult result) 
        {
            foreach (var error in result.Errors) 
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}