using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WRT.Core.BLL;
using WRT.Core.Util;

namespace WRT.Client
{
    public partial class _Race : Page
    {
        private string raceSid;
        private string adminSid;

        protected void Page_Load(object sender, EventArgs e)
        {
            var timer = new TimerService.TimerServiceClient();

            var query = Request.QueryString["race"];
            if (query != null)
            {
                if (query.Length > 3)
                    raceSid = query.Substring(0, 4);
                if (query.Length > 6)
                    adminSid = query.Substring(4, 3);
            }

            if (adminSid == "777")
                buttons.Visible = true;
            else
                buttons.Visible = false;

            if (raceSid != "" && raceSid != null)
            {
                if (!Page.IsPostBack)
                {
                    var race = timer.GetRace(raceSid);

                    if (race.StartTime != null)
                    {
                        btnStartRace.Visible = false;
                        btnStopCompetitor.Visible = true;
                    }
                    else
                    {
                        btnStartRace.Visible = true;
                        btnStopCompetitor.Visible = true;
                    }

                    if (race.RaceSid is null)
                        Response.Redirect("default.aspx");

                    //Om loppet startat - sätt starttid i siten
                    HtmlGenericControl body = (HtmlGenericControl)Master.FindControl("pageBody");
                    HtmlGenericControl header = (HtmlGenericControl)Master.FindControl("headerText");
                    HtmlGenericControl siteid = (HtmlGenericControl)Master.FindControl("siteid");

                    header.InnerText = race.Name;
                    siteid.InnerHtml = "gb.webric.se id:<span style='color: red;'>" + raceSid + "</span>";

                    if (race.StartTime != null)
                    {
                        TimeSpan diff = DateTime.Now.Subtract(DateTime.Parse(race.StartTime.ToString()));

                        var builder = new StringBuilder();
                        builder.Append("startTime(");
                        builder.Append(diff.Hours);
                        builder.Append(",");
                        builder.Append(diff.Minutes);
                        builder.Append(",");
                        builder.Append(diff.Seconds);
                        builder.Append(",true)");
                        body.Attributes.Add("onload", builder.ToString());
                    }
                    else
                    {
                        body.Attributes.Add("onload", "startTime(0,0,0,false)");
                    }
                }
            }
            else
            {
                Response.Redirect("default.aspx");
            }

            //Load competitors
            var competitors = timer.GetCompetitors(raceSid);
            var compList = new List<Core.BLL.DisplayCompetitor>();

            foreach (var comp in competitors)
            {
                var builder = new StringBuilder();

                if (comp.StopTime != null && comp.StartTime != null)
                {

                    TimeSpan diff = DateTime.Parse(comp.StopTime.ToString()).Subtract(DateTime.Parse(comp.StartTime.ToString()));
                    if (diff.Hours.ToString().Length == 1)
                        builder.Append("0" + diff.Hours);
                    else
                        builder.Append(diff.Hours);
                    builder.Append(":");
                    if (diff.Minutes.ToString().Length == 1)
                        builder.Append("0" + diff.Minutes);
                    else
                        builder.Append(diff.Minutes);
                    builder.Append(":");
                    if (diff.Seconds.ToString().Length == 1)
                        builder.Append("0" + diff.Seconds);
                    else
                        builder.Append(diff.Seconds);
                }
                else
                    builder.Append("");

                compList.Add(new DisplayCompetitor(comp.CompetitorSid, comp.Name, builder.ToString()));
            }

            rptCompetitors.DataSource = compList;
            rptCompetitors.DataBind();
        }

        protected void BtnAddCompetitor_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("NewCompetitor.aspx?race=" + raceSid);
        }

        protected void BtnStopCompetitor_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("StopCompetitor.aspx?race=" + raceSid);
        }


        protected void BtnStartRace_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("Startrace.aspx?race=" + raceSid);
        }
    }
}