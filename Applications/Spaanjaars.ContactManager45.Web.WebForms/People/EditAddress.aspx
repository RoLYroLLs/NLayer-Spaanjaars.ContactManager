<%@ Page Title="Edit address" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditAddress.aspx.cs" Inherits="Spaanjaars.ContactManager45.Web.WebForms.People.EditAddress" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <div>
    <table class="auto-style1">
      <tr>
        <td>Street</td>
        <td>
          <asp:TextBox ID="Street" runat="server"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td>Zip Code</td>
        <td>
          <asp:TextBox ID="ZipCode" runat="server"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td>City</td>
        <td>
          <asp:TextBox ID="City" runat="server"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td>Country</td>
        <td>
          <asp:TextBox ID="Country" runat="server"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td>
          <asp:Button ID="SaveButton" runat="server" OnClick="SaveButton_Click" Text="Save" />
          <a href="Default.aspx">Back to List</a></td>
      </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowModelStateErrors="true" ForeColor="Red" HeaderText="Please check the following errors:" />
  </div>
</asp:Content>
