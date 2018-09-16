<%@ Page Title="E-mail addresses" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmailAddresses.aspx.cs" Inherits="Spaanjaars.ContactManager45.Web.WebForms.People.EmailAddresses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <asp:GridView ID="EmailAddressesGrid" runat="server" AutoGenerateColumns="false"
    AllowSorting="true" AllowPaging="true" PageSize="5" DataKeyNames="Id, ContactType, OwnerId" EmptyDataText="No e-mail addresses found."
    ItemType="Spaanjaars.ContactManager45.Model.EmailAddress" GridLines="None" CellSpacing="5"
    SelectMethod="ListEmailAddresses" DeleteMethod="DeleteEmailAddress" UpdateMethod="UpdateEmailAddress" OnRowUpdating="EmailAddressesGrid_RowUpdating">
    <Columns>
      <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" ReadOnly="True" />
      <asp:BoundField DataField="EmailAddressText" HeaderText="E-mail address" SortExpression="EmailAddressText" />
      <asp:TemplateField HeaderText="ContactType" SortExpression="ContactType">
        <EditItemTemplate>
          <asp:DropDownList runat="server" ID="ContactType" SelectedValue='<%# (int)(Item.ContactType) %>' SelectMethod="GetContactTypes" DataValueField="Value" DataTextField="Text"></asp:DropDownList>
        </EditItemTemplate>
        <ItemTemplate>
          <asp:Label ID="Label1" runat="server" Text='<%# Item.ContactType %>'></asp:Label>
        </ItemTemplate>
      </asp:TemplateField>
      <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
    </Columns>
  </asp:GridView>
  <br />
  <br />
  <asp:DetailsView ID="DetailsView1" DataKeyNames="Id" runat="server" GridLines="None" CellSpacing="5" InsertMethod="InsertEmailAddress" DefaultMode="Insert" SelectMethod="ListEmailAddresses" OnItemInserted="DetailsView1_ItemInserted" AutoGenerateRows="False" ItemType="Spaanjaars.ContactManager45.Model.EmailAddress">
    <Fields>
      <asp:BoundField DataField="EmailAddressText" HeaderText="EmailAddressText" />
      <asp:TemplateField HeaderText="ContactType">
        <InsertItemTemplate>
          <asp:DropDownList runat="server" ID="ContactType" SelectedValue='<%# BindItem.ContactType %>' SelectMethod="GetContactTypes" DataValueField="Value" DataTextField="Text"></asp:DropDownList>
        </InsertItemTemplate>
      </asp:TemplateField>
      <asp:CommandField ShowInsertButton="True" />
    </Fields>
  </asp:DetailsView>

  <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowModelStateErrors="true" ForeColor="Red" HeaderText="Please check the following errors:" />
  <br />
  <br />
  <a href="Default.aspx">Back to people list</a>
</asp:Content>
