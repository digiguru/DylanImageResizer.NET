<%@ Page Title="Home Page" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="Default.aspx.vb" Inherits="ExampleSite._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Image resizer example!
    </h2>

    <h3>From local path</h3>
    <p>
       Original
       <br />
       <asp:image id="imgOriginal" runat="server" />
    </p>
    <p>
       Resized Large
       <br />
       <asp:image id="imgResized1" runat="server" />
    </p>
    <p>
       Resized Small
       <br />
       <asp:image id="imgResized2" runat="server" />
    </p>
    <hr />
    <h3>From actual path</h3>
    <p>
       Original
       <br />
       <asp:image id="imgOriginalFile" runat="server" />
    </p>
    <p>
       Resized Large
       <br />
       <asp:image id="imgResizedFile1" runat="server" />
    </p>
    <p>
       Resized Small
       <br />
       <asp:image id="imgResizedFile2" runat="server" />
    </p>
    <hr />
    <h3>From webserver</h3>
    <p>
       Original
       <br />
       <asp:image id="imgOriginalWeb" runat="server" />
    </p>
    <p>
       Resized Large
       <br />
       <asp:image id="imgResizedWeb1" runat="server" />
    </p>
    <p>
       Resized Small
       <br />
       <asp:image id="imgResizedWeb2" runat="server" />
    </p>

</asp:Content>
