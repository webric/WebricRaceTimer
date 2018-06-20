<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StopCompetitor.aspx.cs" Inherits="WRT.Client._StopCompetitor" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
    <meta http-equiv="refresh" content="60">
</asp:Content>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div style="text-align: center">
        <p>
            <label>Nummer</label><br />
            <asp:TextBox runat="server" ID="txtCompetitorNumber" Width="250px"></asp:TextBox><br />
            <asp:Button runat="server" ID="btnStopCompetitor" Text="Löparen går i mål" Width="250px" OnClick="BtnStopCompetitor_OnClick" /><br />
        </p>
    </div>
    <meta content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;" name="viewport" />
</asp:Content>
