<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WRT.Client._Default" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
    <meta http-equiv="refresh" content="60">
</asp:Content>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div style="text-align: center">
         <h2>Skriv in lopp id</h2>
        <p>
            <asp:TextBox runat="server" ID="txtExistingRaceSid" Width="200px"></asp:TextBox><br />
            <asp:Button runat="server" ID="btnExistingRaceSid" Text="Öppna loppet" Width="200px" OnClick="BtnExistingRaceSid_OnClick" /><br />
        </p>
        <%--<h2>Nytt lopp</h2>
        <p>
            <asp:Button runat="server" ID="btnNewRace" Text="Skapa nytt loop" Width="200px" OnClick="BtnNewRace_OnClick" /><br />
        </p>--%>
    </div>
    <meta content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;" name="viewport" />
</asp:Content>
