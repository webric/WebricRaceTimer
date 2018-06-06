<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WRT.Client._Default" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
    <meta http-equiv="refresh" content="60">
</asp:Content>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div style="text-align: center">
        <h2>Befintligt loop</h2>
        <p>
            <asp:DropDownList runat="server" ID="ddlKontainrar" Width="250px" OnSelectedIndexChanged="ddlKontainrar_OnSelectedIndexChanged" AutoPostBack="True" />
        </p>
        <h2>Nytt lopp</h2>
        <p>
            <asp:Button runat="server" ID="btnStatusTom" Text="Jag är tom" Width="250px" OnClick="btnStatusTom_OnClick" /><br />
            <asp:Button runat="server" ID="btnStatusHalv" Text="Jag är halvfull" Width="250px" OnClick="btnStatusHalv_OnClick" /><br />
            <asp:Button runat="server" ID="btnStatusFull" Text="Jag är full" Width="250px" OnClick="btnStatusFull_OnClick" /><br />
            <asp:Button runat="server" ID="btnStatusLost" Text="Jag är lost" Width="250px" OnClick="btnStatusLost_OnClick" />
        </p>
        <h2>Var är jag?</h2>
        <p>
            Longitud:&nbsp;<asp:Label ID="lblLongitud" runat="server"></asp:Label>
            <br />
            Latitud:&nbsp;<asp:Label ID="lblLatitude" runat="server"></asp:Label>
            <br />
            Noggrannhet:&nbsp;<asp:Label ID="lblNoggranhet" runat="server"></asp:Label>
        </p>
        <h2>Pinga</h2>
        <p>
            <asp:Button runat="server" ID="btnSkickaPing" Text="Skicka bara koordinater" Width="250px" OnClick="btnSkickaPing_OnClick" />
        </p>

        <div id="divMap">Ingen karta kan visas</div>
        <asp:HiddenField runat="server" ID="hidLongitude" />
        <asp:HiddenField runat="server" ID="hidLatutude" />
        <asp:HiddenField runat="server" ID="hidNoggranhet" />
    </div>
    <meta content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;" name="viewport" />
    <script>
        function successHandler(location) {
            document.getElementById("MainContent_lblLongitud").innerHTML = location.coords.longitude;
            document.getElementById("MainContent_hidLongitude").value = location.coords.longitude;
            document.getElementById("MainContent_lblLatitude").innerHTML = location.coords.latitude;
            document.getElementById("MainContent_hidLatutude").value = location.coords.latitude;
            document.getElementById("MainContent_lblNoggranhet").innerHTML = location.coords.accuracy;
            document.getElementById("MainContent_hidNoggranhet").value = location.coords.accuracy;

            var map = document.getElementById("divMap"), html = [];
            html.push("<img width='256' height='256' src='http://maps.google.com/maps/api/staticmap?center=", location.coords.latitude, ",", location.coords.longitude, "&markers=size:small|color:blue|", location.coords.latitude, ",", location.coords.longitude, "&zoom=14&size=250x250&sensor=false' />");
            map.innerHTML = html.join("");
        }
        function errorHandler(error) {
            alert('Attempt to get location failed: ' + error.message);
        }
        navigator.geolocation.getCurrentPosition(successHandler, errorHandler);
    </script>
    <script>
        function ClickButton() {
            //sleep(300000000000);

            //document.getElementById('MainContent_btnSkickaPing').click();
        }
        function sleep(milliseconds) {
            var start = new Date().getTime();
            for (var i = 0; i < 1e7; i++) {
                if ((new Date().getTime() - start) > milliseconds) {
                    break;
                }
            }
        }
    </script>
</asp:Content>
