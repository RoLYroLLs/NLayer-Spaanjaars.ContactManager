<%@ Page Title="Phone numbers" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PhoneNumbers.aspx.cs" Inherits="Spaanjaars.ContactManager45.Web.WebForms.People.PhoneNumbers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <asp:GridView ID="PhoneNumbersGrid" runat="server" AutoGenerateColumns="False"
    AllowSorting="True" AllowPaging="True" PageSize="5" DataKeyNames="Id,ContactType,OwnerId"
    ItemType="Spaanjaars.ContactManager45.Model.PhoneNumber" GridLines="None" CellSpacing="5" EmptyDataText="No phone numbers found."
    SelectMethod="ListPhoneNumbers" DeleteMethod="DeletePhoneNumber" UpdateMethod="UpdatePhoneNumber" OnRowUpdating="PhoneNumbersGrid_RowUpdating">
    <Columns>
      <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" ReadOnly="True" />
      <asp:BoundField DataField="Number" HeaderText="Number" SortExpression="Number" />
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
  <asp:DetailsView ID="DetailsView1" DataKeyNames="Id" runat="server" GridLines="None" CellSpacing="5" InsertMethod="InsertPhoneNumber" DefaultMode="Insert" SelectMethod="ListPhoneNumbers" OnItemInserted="DetailsView1_ItemInserted" AutoGenerateRows="False" ItemType="Spaanjaars.ContactManager45.Model.PhoneNumber">
    <Fields>
      <asp:BoundField DataField="Number" HeaderText="Number" />
      <asp:TemplateField HeaderText="ContactType">
        <InsertItemTemplate>
          <asp:DropDownList runat="server" ID="ContactType" SelectedValue='<%# BindItem.ContactType %>' SelectMethod="GetContactTypes" DataValueField="Value" DataTextField="Text"></asp:DropDownList>
        </InsertItemTemplate>
      </asp:TemplateField>
      <asp:CommandField ShowInsertButton="True" />
    </Fields>
  </asp:DetailsView>
  <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowModelStateErrors="true" ForeColor="Red" HeaderText="Please check the following errors:" />
</asp:Content>
