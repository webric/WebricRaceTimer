using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WRT.Core.BLL;
using WRT.Core.Util;

namespace WRT.Client
{
    public partial class _StopCompetitor : Page
    {
        private string raceSid;

        protected void Page_Load(object sender, EventArgs e)
        {
            raceSid = Request.QueryString["race"];

            if (raceSid == "" || raceSid == null)
            {
                Response.Redirect("default.aspx");
            }
            else
            {
                var timer = new TimerService.TimerServiceClient();
                var race = timer.GetRace(raceSid);

                if (race.RaceSid is null)
                    Response.Redirect("default.aspx");

                HtmlGenericControl header = (HtmlGenericControl)Master.FindControl("headerText");
                HtmlGenericControl siteid = (HtmlGenericControl)Master.FindControl("siteid");

                header.InnerText = race.Name;
                siteid.InnerHtml = "gb.webric.se id:<span style='color: red;'>" + raceSid + "</span>";
            }
        }

        protected void BtnStopCompetitor_OnClick(object sender, EventArgs e)
        {
            var timer = new TimerService.TimerServiceClient();
            timer.FinnishCompetitor(raceSid, txtCompetitorNumber.Text);

            txtCompetitorNumber.Text = "";
        }
        protected void BtnToRace_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("race.aspx?race=" + raceSid);
        }
    }
}