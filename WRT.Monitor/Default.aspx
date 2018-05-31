<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="IPS.Monitor._Default" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
    <script src="http://maps.googleapis.com/maps/api/js?key=AIzaSyBjJ2GqtZh30u4OBF52GDD5K56NQWGr-YQ&sensor=false">
    </script>
    <link rel="stylesheet" href="/Content/ui.datepicker.css" type="text/css" media="screen" />
</asp:Content>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1>Övervaning av kontainrar</h1>
            </hgroup>
            <p>
                Nedan visas se senaste positionsrapporteringarna. Här kan det byggas in sökning på kontainer, tidpunkt, status (tom, full osv). Självklart kan denna sida också anpassas att visa vissa kontainrar för vissa användare osv.
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/jquery.js" />
            <asp:ScriptReference Path="~/Scripts/ui.datepicker.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <script>
        Sys.Application.add_load(function() {
            $(".dpicker").datepicker({
                dateFormat: $.datepicker.W3C,
                showStatus: true,
                firstDay: 1,
                hideIfNoPrevNext: true,
                flat: true,
                showOn: "both",
                buttonImage: "images/DatePicker/calendar.gif",
                buttonImageOnly: true,
                onSelect: function() {
                    $('#<%=txtFrom.ClientID %>').trigger('change');
                }
            });
            $.datepicker.regional['sv'] = {
                clearText: 'Rensa', clearStatus: '',
                closeText: 'Stäng', closeStatus: '',
                prevText: '&laquo;Förra', prevStatus: '',
                nextText: 'Nästa&raquo;', nextStatus: '',
                currentText: 'Idag', currentStatus: '',
                monthNames: ['Januari', 'Februari', 'Mars', 'April', 'Maj', 'Juni',
    'Juli', 'Augusti', 'September', 'Oktober', 'November', 'December'],
                monthNamesShort: ['Jan', 'Feb', 'Mar', 'Apr', 'Maj', 'Jun',
    'Jul', 'Aug', 'Sep', 'Okt', 'Nov', 'Dec'],
                monthStatus: '', yearStatus: '',
                weekHeader: 'Ve', weekStatus: '',
                dayNamesShort: ['Sön', 'Mån', 'Tis', 'Ons', 'Tor', 'Fre', 'Lör'],
                dayNames: ['Söndag', 'Måndag', 'Tisdag', 'Onsdag', 'Torsdag', 'Fredag', 'Lördag'],
                dayNamesMin: ['Sö', 'Må', 'Ti', 'On', 'To', 'Fr', 'Lö'],
                dayStatus: 'DD', dateStatus: 'D, M d',
                dateFormat: 'yy-mm-dd', firstDay: 0,
                initStatus: '', isRTL: false
            };
            $.datepicker.setDefaults($.datepicker.regional['sv']);
        }); 
    </script>
    <h3>Senaste positionsrapporteringarna</h3>
    <p>Här finns det möjlighet att rita ut exempelvis senaste dygnets koordinater för en kontainer, visa alla kontainrar som är fulla eller tillhör en viss kategori, visa kontainrar som flyttats senaste veckan, zooma och flytta kartan på alla sätt.</p>
    <asp:DropDownList runat="server" ID="ddlKontainrar" Width="150" OnSelectedIndexChanged="ddlKontainrar_OnSelectedIndexChanged" AutoPostBack="True" />&nbsp;&nbsp;&nbsp;&nbsp;
    From&nbsp;<asp:TextBox runat="server" ID="txtFrom" Width="150" CssClass="dpicker" OnTextChanged="txtFrom_OnTextChanged"></asp:TextBox>&nbsp;
    Tom&nbsp;<asp:TextBox runat="server" ID="txtTom" Width="150" CssClass="dpicker" OnTextChanged="txtTom_OnTextChanged"></asp:TextBox>&nbsp;<asp:Button runat="server" ID="btnSök" OnClick="btnSök_OnClick" Text="Sök"/>
    <table>
        <tr>
            <td valign="top">
                <table>
                    <tr>
                        <td>&nbsp;</td>
                        <td>Kontainer</td>
                        <td>Tidpunkt</td>
                        <td>Status</td>
                        <!--<td>&nbsp;</td>-->
                    </tr>

                    <asp:Repeater runat="server" ID="rpSenastePositionerna">

                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("Radnummer") %></td>
                                <td><%#Eval("Namn") %> (<%#Eval("Serienummer") %>)</td>
                                <td><%#DateTime.Parse(Eval("Tidpunkt").ToString()).ToString("yyMMdd HH:mm") %></td>
                                <td><%#Eval("Status") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>

            </td>
            <td valign="top">
                <script type="text/javascript">
                    var markers = [
                        <asp:Repeater ID="rptMarkers" runat="server">
                            <ItemTemplate>
                                    {
                                        "title": '<%# Eval("Namn") %>',
                                        "lat": '<%# Eval("Latitude") %>',
                                        "lng": '<%# Eval("Longitude") %>',
                                        "description": 'Typ: <%# Eval("Namn") %><br/>Serienummer: <%# Eval("SerieNummer") %><br/>Status: <%# Eval("Status") %><br/>Tidpunkt: <%# Eval("Tidpunkt") %>',
                                        "category" : '<%# Eval("Status") %>',
                                        "rownr" : '<%# Eval("Radnummer") %>',
                                        "ani" : '<%# Eval("Special") %>'
                                    }
                            </ItemTemplate>
                            <SeparatorTemplate>
                                    ,
                            </SeparatorTemplate>
                    </asp:Repeater>
                    ];
                </script>
                <script type="text/javascript">
                    window.onload = function () {
                        var mapOptions = {
                            //TODO - sätt fokus på senaste punkten
                            center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                            //TODO - sätt zoom så att alla involverade punkter får plats
                            zoom: 11,
                            mapTypeId: google.maps.MapTypeId.ROADMAP
                        };
                        var infoWindow = new google.maps.InfoWindow();
                        
                        var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);

                        //Definiera sökvägar till ikonerna
                        var categoryIcons = {
                            "0": "https://chart.googleapis.com/chart?chst=d_map_pin_letter&chld=1%7CFF0000%7C000000",
                            "1": "https://chart.googleapis.com/chart?chst=d_map_pin_letter&chld=2%7C00FFFF%7C000000",
                            "2": "https://chart.googleapis.com/chart?chst=d_map_pin_letter&chld=3%7CFF00FF%7C000000",
                            "": "https://chart.googleapis.com/chart?chst=d_map_pin_letter&chld=3%7CFF00FF%7C000000"
                        };

                        //Linje
                        var rutLinje = new Array();
                        
                        //Lägg in alla positioner
                        for (i = 0; i < markers.length; i++) {
                            var data = markers[i];
                            var myLatlng = new google.maps.LatLng(data.lat, data.lng);
                            var ddlKon = document.getElementById("MainContent_ddlKontainrar");
                            

                            //Animation
                            var ani = "";
                            if (ddlKon.options[ddlKon.selectedIndex].value != "") {
                                if (i == 0) {
                                    ani = google.maps.Animation.BOUNCE;
                                } else {
                                    ani = '';
                                }
                            } else {
                                ani = google.maps.Animation.BOUNCE;
                            }

                            //Skapa marker
                            var marker = new google.maps.Marker({
                                position: myLatlng,
                                map: map,
                                title: data.title,
                                icon: categoryIcons[data.category],
                                animation: ani
                            });
                            //Lägg till marker
                            (function (marker, data) {
                                google.maps.event.addListener(marker, "click", function (e) {
                                    infoWindow.setContent(data.description);
                                    infoWindow.open(map, marker);
                                });
                            })(marker, data);

                            //Lägg till position för att rita ut linje
                            //Bara ifall ddlKontainrar <> "Alla"
                            if(ddlKon.options[ddlKon.selectedIndex].value !="")
                                rutLinje.push(myLatlng);
                        }
                        
                        //Lägg in polygonlinje mellan markers
                        var flightPath = new google.maps.Polyline({
                            path: rutLinje,
                            strokeColor: "#FF0000",
                            strokeOpacity: 1.0,
                            strokeWeight: 2
                        });

                        flightPath.setMap(map);
                    }
                </script>
                <div id="dvMap" style="width: 480px; height: 400px">
                    <div id="googleMap" style="width: 400px; height: 280px;" visible="false"></div>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <div id="divMap"></div>
    <asp:HiddenField runat="server" ID="hidKoordinater" />
    <script>
        function ritaOmKartan(longi, lati) {
            var map = document.getElementById("divMap"), html = [];
            html.push("<img width='400' height='400' src='http://maps.google.com/maps/api/staticmap?center=", lati, ",", longi, "&markers=size:small|color:blue|", lati, ",", longi, "&zoom=14&size=400x400&sensor=false' />");
            map.innerHTML = html.join("");
        }
    </script>

</asp:Content>
