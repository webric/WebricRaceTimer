<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewRace.aspx.cs" Inherits="WRT.Client._NewRace" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
    <meta http-equiv="refresh" content="60">
</asp:Content>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div style="text-align: center">
        <h2>Skapa nytt lopp</h2>
        <p>
            <label>Namn</label><br />
            <asp:TextBox runat="server" ID="txtRaceName" Width="200px"></asp:TextBox><br />
            <asp:Button runat="server" ID="btnSaveNewRace" Text="Skapa nytt lopp" Width="200px" OnClick="BtnSaveNewRace_OnClick" /><br />
        </p>
    </div>
    <meta content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;" name="viewport" />
</asp:Content>
