<%@ Page Title="Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="Spaanjaars.ContactManager45.Web.WebForms.People.Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <table class="auto-style1">
    <tr>
      <td>First name</td>
      <td>
        <asp:Label ID="FirstName" runat="server"></asp:Label>
      </td>
    </tr>
    <tr>
      <td>Last name</td>
      <td>
        <asp:Label ID="LastName" runat="server"></asp:Label>
      </td>
    </tr>
    <tr>
      <td>Date of birth</td>
      <td>
        <asp:Label ID="DateOfBirth" runat="server"></asp:Label>
      </td>
    </tr>
    <tr>
      <td>Type</td>
      <td>
        <asp:Label ID="Type" runat="server"></asp:Label>
      </td>
    </tr>
  </table>
  <br />
  <a href="Default.aspx">Back to List</a>
</asp:Content>
