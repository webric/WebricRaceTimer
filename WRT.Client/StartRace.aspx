<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StartRace.aspx.cs" Inherits="WRT.Client._StartRace" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
    <meta http-equiv="refresh" content="60">
</asp:Content>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div style="text-align: center">
        <p>
            <asp:Button runat="server" ID="btnStartRace" Text="Starta loppet" Width="200px" OnClick="BtnStartRace_OnClick" /><br />
        </p>
    </div>
    <meta content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;" name="viewport" />
</asp:Content>
