<%@ Page Title="CompanyPriceListEdit" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Edit.aspx.cs" Inherits="PriceList.Views.PriceLists.Edit" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<p>&nbsp;</p>
        <asp:FormView runat="server"
            ItemType="PriceList.Models.CompanyPriceList" DefaultMode="Edit" DataKeyNames="ID"
            UpdateMethod="UpdateItem" SelectMethod="GetItem"
            OnItemCommand="ItemCommand" RenderOuterTable="false">
            <EmptyDataTemplate>
                Cannot find the CompanyPriceList with ID <%: Request.QueryString["ID"] %>
            </EmptyDataTemplate>
            <EditItemTemplate>
                <fieldset class="form-horizontal">
                    <legend>Edit CompanyPriceList</legend>
					<asp:ValidationSummary runat="server" CssClass="alert alert-danger"  />                 
						    <asp:DynamicControl Mode="Edit" DataField="CreatedDate" runat="server" />
							<asp:DynamicControl Mode="Edit" 
								DataField="DeviceID" 
								DataTypeName="PriceList.Models.Device" 
								DataTextField="Specification" 
								DataValueField="ID" 
								UIHint="ForeignKey" runat="server" />
						    <asp:DynamicControl Mode="Edit" DataField="Amount" runat="server" />
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
							<asp:Button runat="server" ID="UpdateButton" CommandName="Update" Text="Update" CssClass="btn btn-primary" />
							<asp:Button runat="server" ID="CancelButton" CommandName="Cancel" Text="Cancel" CausesValidation="false" CssClass="btn btn-default" />
						</div>
					</div>
                </fieldset>
            </EditItemTemplate>
        </asp:FormView>
    </div>
</asp:Content>

