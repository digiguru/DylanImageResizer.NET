<%@ Page Title="Home Page" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="Default.aspx.vb" Inherits="ExampleSite._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Image resizer example!
    </h2>
    <p>
       Original
       <br />
       <asp:image id="imgOriginal" runat="server" />
    </p>
    <p>
       Resized
       <br />
       <asp:image id="imgResized" runat="server" />
    </p>
</asp:Content>
