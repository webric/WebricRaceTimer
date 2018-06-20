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

        protected void Page_Load(object sender, EventArgs e)
        {
            raceSid = Request.QueryString["race"];

            if (raceSid != "" && raceSid != null)
            {
                if (!Page.IsPostBack)
                {
                    var timer = new TimerService.TimerServiceClient();
                    var race = timer.GetRace(raceSid);

                    if (race.RaceSid is null)
                        Response.Redirect("default.aspx");

                    //Om loppet startat - sätt starttid i siten
                    HtmlGenericControl body = (HtmlGenericControl)Master.FindControl("pageBody");
                    HtmlGenericControl header = (HtmlGenericControl)Master.FindControl("headerText");
                    HtmlGenericControl siteid = (HtmlGenericControl)Master.FindControl("siteid");

                    header.InnerText = race.Name;
                    siteid.InnerHtml = "gb.webric.se id: <span style='color: red;'>" + raceSid + "</span>";

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
            //competitorTable.
            competitorTable.DataBind();
        }

        protected void BtnAddCompetitor_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("/NewCompetitor.aspx");
        }
    }
}