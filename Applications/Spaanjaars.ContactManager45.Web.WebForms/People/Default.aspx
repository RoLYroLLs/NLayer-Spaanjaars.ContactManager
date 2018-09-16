<%@ Page Title="Contact People" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Spaanjaars.ContactManager45.Web.WebForms.People.Default1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <asp:GridView ID="categoriesGrid" runat="server" AutoGenerateColumns="false"
    AllowSorting="true" AllowPaging="true" PageSize="10"
    ItemType="Spaanjaars.ContactManager45.Model.Person"
    SelectMethod="ListPeople" DeleteMethod="DeletePerson" GridLines="None" CellSpacing="5" EmptyDataText="No contact people found.">
    <Columns>
      <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" />
      <asp:HyperLinkField DataNavigateUrlFields="Id" HeaderText="Full name" SortExpression="FirstName" DataNavigateUrlFormatString="Details.aspx?Id={0}" DataTextField="FullName"></asp:HyperLinkField>
      <asp:BoundField DataField="DateOfBirth" HeaderText="Date of Birth" SortExpression="DateOfBirth" DataFormatString="{0:d}" />
      <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
      <asp:TemplateField HeaderText="Addresses">
        <ItemTemplate>
          <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("EditAddress.aspx?Id={0}&ContactType=2", Item.Id) %>' Text="Home"></asp:HyperLink>
          |          
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("EditAddress.aspx?Id={0}&ContactType=1", Item.Id) %>' Text="Work"></asp:HyperLink>
        </ItemTemplate>
      </asp:TemplateField>
      <asp:HyperLinkField DataNavigateUrlFields="Id" HeaderText="" DataNavigateUrlFormatString="EmailAddresses.aspx?Id={0}" Text="E-mail addresses"></asp:HyperLinkField>
      <asp:HyperLinkField DataNavigateUrlFields="Id" HeaderText="" DataNavigateUrlFormatString="PhoneNumbers.aspx?Id={0}" Text="Phone numbers"></asp:HyperLinkField>
      <asp:HyperLinkField DataNavigateUrlFields="Id" HeaderText="" DataNavigateUrlFormatString="AddEditPerson.aspx?Id={0}" Text="Edit"></asp:HyperLinkField>
      <asp:CommandField ShowDeleteButton="True" />
    </Columns>
  </asp:GridView>
  <br />
  <br />
  <a href="AddEditPerson.aspx">Create new contact person</a>
</asp:Content>
