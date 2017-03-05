<%@ Page Title="Register an external login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterExternalLogin.aspx.cs" Inherits="PriceList.Account.RegisterExternalLogin" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
<h3>Register with your <%: ProviderName %> account</h3>

    <asp:PlaceHolder runat="server">
        <div class="form-horizontal">
            <h4>Association Form</h4>
            <hr />
            <asp:ValidationSummary runat="server" ShowModelStateErrors="true" CssClass="text-danger" />
            <p class="text-info">
                You've authenticated with <strong><%: ProviderName %></strong>. Please enter an email below for the current site
                and click the Log in button.
            </p>
            <div class="form-group">
            <asp:Label runat="server" CssClass="col-md-2 control-label">Type</asp:Label>
            <asp:DropDownList  DataField="ID"  ID="drpType"
								DataTypeName="PriceList.Models.UserType" 
								DataTextField="Name" 
								DataValueField="ID"  runat="server" />
        </div>
        <div class="form-group">
             <asp:Label runat="server" AssociatedControlID="FirstName" CssClass="col-md-2 control-label">First Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="FirstName" CssClass="form-control" TextMode="SingleLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="FirstName"
                    CssClass="text-danger" ErrorMessage="The FirstName field is required." />
            </div>
        </div>
        <div class="form-group">
             <asp:Label runat="server" AssociatedControlID="CompanyName" CssClass="col-md-2 control-label">Company Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="CompanyName" CssClass="form-control" TextMode="SingleLine" />
            </div>
        </div>
         <div class="form-group">
             <asp:Label runat="server" AssociatedControlID="Contacts" CssClass="col-md-2 control-label">Contacts</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Contacts" CssClass="form-control" TextMode="SingleLine" />
            </div>
        </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="email" CssClass="col-md-2 control-label">Email</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="email" CssClass="form-control" TextMode="Email" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="email"
                        Display="Dynamic" CssClass="text-danger" ErrorMessage="Email is required" />
                    <asp:ModelErrorMessage runat="server" ModelStateKey="email" CssClass="text-error" />
                </div>
            </div>

            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <asp:Button runat="server" Text="Log in" CssClass="btn btn-default" OnClick="LogIn_Click" />
                </div>
            </div>
        </div>
    </asp:PlaceHolder>
</asp:Content>
