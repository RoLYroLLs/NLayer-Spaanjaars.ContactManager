<%@ Page Title="ContactManager" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddEditPerson.aspx.cs" Inherits="Spaanjaars.ContactManager45.Web.WebForms.People.AddEditPerson" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
  <style type="text/css">
    .auto-style1 {
      width: 100%;
    }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <table class="auto-style1">
    <tr>
      <td>First name</td>
      <td>
        <asp:TextBox ID="FirstName" runat="server"></asp:TextBox>
      </td>
    </tr>
    <tr>
      <td>Last name</td>
      <td>
        <asp:TextBox ID="LastName" runat="server"></asp:TextBox>
      </td>
    </tr>
    <tr>
      <td>Date of birth</td>
      <td>
        <asp:TextBox ID="DateOfBirth" runat="server"></asp:TextBox>
      </td>
    </tr>
    <tr>
      <td>Type</td>
      <td>
        <asp:DropDownList ID="Type" runat="server" SelectMethod="GetTypes" DataValueField="Value" DataTextField="Text">
        </asp:DropDownList>
      </td>
    </tr>
    <tr>
      <td>&nbsp;</td>
      <td>
        <asp:Button ID="SaveButton" runat="server" OnClick="SaveButton_Click" Text="Save" />
        <a href="Default.aspx">Back to List</a></td>
    </tr>
  </table>
  <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowModelStateErrors="true" ForeColor="Red" HeaderText="Please check the following errors:" />
</asp:Content>
