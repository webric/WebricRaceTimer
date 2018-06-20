<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Race.aspx.cs" Inherits="WRT.Client._Race" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
    <meta http-equiv="refresh" content="60">
</asp:Content>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div style="text-align: center">
        <div id='seconds-counter' style='font-size: 80px; font-weight: bolder; font-family: Verdana, Helvetica, Sans-Serif;'></div>
    </div>
    <meta content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;" name="viewport" />
    <script>
        function startTime(hours, minutes, seconds, running) {

            var el = document.getElementById('seconds-counter');

            function incrementSeconds() {
                if (running) { seconds += 1; }

                //add minutes or hours when needed
                if (seconds == 60) {
                    seconds = 0;
                    minutes += 1;
                }
                if (minutes == 60) {
                    minutes = 0;
                    hours += 1;
                }

                //init display objects
                var hourDisplay = hours;
                var minutesDisplay = minutes;
                var secondsDisplay = seconds;

                //add 0 if only one letter
                if (hours.toString().length == 1) { hourDisplay = "0" + hourDisplay; }
                if (minutes.toString().length == 1) { minutesDisplay = "0" + minutesDisplay; }
                if (seconds.toString().length == 1) {
                    secondsDisplay = "0" + secondsDisplay;
                }

                el.innerText = hourDisplay + ":" + minutesDisplay + ":" + secondsDisplay;
            }

            var cancel = setInterval(incrementSeconds, 1000);
        }
    </script>
    <br />
    <table style="margin: auto;">
        <thead>
            <tr>
                <td><b>Nummer</b></td>
                <td><b>Namn</b></td>
                <td><b>Sluttid</b></td>
            </tr>
        </thead>
        <asp:Repeater runat="server" ID="rptCompetitors">
            <ItemTemplate>
                <tr>
                    <td><%# Eval("CompetitorSid") %></td>
                    <td><%# Eval("Name") %></td>
                    <td><%# Eval("EndTime") %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <br />
     <div style="text-align: center">
        <p>
     <asp:Button runat="server" ID="btnAddCompetitor" Text="Ny löpare" Width="250px"  OnClick="BtnAddCompetitor_OnClick" /><br />
    <asp:Button runat="server" ID="btnStopCompetitor" Text="Stoppa löpare" Width="250px" OnClick="BtnStopCompetitor_OnClick" /><br />
   </p></div>
</asp:Content>
