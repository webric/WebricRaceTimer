<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewCompetitor.aspx.cs" Inherits="WRT.Client._NewCompetitor" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
    <meta http-equiv="refresh" content="60">
</asp:Content>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div style="text-align: center">
        <h3>Bekräftelse</h3>
        <p>
            <asp:Label runat="server" ID="lblComfirmationText"></asp:Label>
        </p>
        <h2>Ny deltagare</h2>
        <p>
            <label>Nummer</label><br />
            <asp:TextBox runat="server" ID="txtCompetitorNumber" Width="250px"></asp:TextBox><br />
            <label>Namn</label><br />
            <asp:TextBox runat="server" ID="txtCompetitorName" Width="250px"></asp:TextBox><br />
            <asp:Button runat="server" ID="btnSaveNewCompetitor" Text="Spara" Width="250px" OnClick="BtnSaveNewCompetitor_OnClick" /><br />
        </p>
    </div>
    <meta content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;" name="viewport" />
</asp:Content>
