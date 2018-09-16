<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SimpleExample.aspx.cs" Inherits="Spaanjaars.ContactManager45.Web.WebForms.People.SimpleExample" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <asp:GridView ID="GridView1" runat="server" DataKeyNames="Id" ItemType="Spaanjaars.ContactManager45.Model.Person" SelectMethod="FindAll" UpdateMethod="UpdatePerson" DeleteMethod="DeletePerson">
    <Columns>
      <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
    </Columns>
  </asp:GridView>
  <asp:DetailsView ID="DetailsView1" runat="server" ItemType="Spaanjaars.ContactManager45.Model.Person" SelectMethod="FindAll" InsertMethod="InsertPerson" DefaultMode="Insert">
    <Fields>
      <asp:CommandField ShowInsertButton="True" />
    </Fields>
  </asp:DetailsView>
</asp:Content>
