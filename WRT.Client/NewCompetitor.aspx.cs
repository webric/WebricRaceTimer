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
    public partial class _NewCompetitor : Page
    {
        private string raceSid;

        protected void Page_Load(object sender, EventArgs e)
        {
            raceSid = Request.QueryString["race"];

            if (raceSid == "" || raceSid == null)
            {
                Response.Redirect("default.aspx");
            }

            var timer = new TimerService.TimerServiceClient();
            var race = timer.GetRace(raceSid);

            HtmlGenericControl header = (HtmlGenericControl)Master.FindControl("headerText");
            HtmlGenericControl siteid = (HtmlGenericControl)Master.FindControl("siteid");

            header.InnerText = race.Name;
            siteid.InnerHtml = "gb.webric.se id:<span style='color: red;'>" + raceSid + "</span>";
        }

        protected void BtnSaveNewCompetitor_OnClick(object sender, EventArgs e)
        {
            if (txtCompetitorNumber.Text != "" && txtCompetitorName.Text != "")
            {
                var timer = new TimerService.TimerServiceClient();
                var competitor = timer.CreateCompetitor(txtCompetitorNumber.Text, txtCompetitorName.Text, raceSid);

                lblComfirmationText.Text = string.Format("{0}, {1} tillagd", competitor.CompetitorSid, competitor.Name);
                txtCompetitorNumber.Text = "";
                txtCompetitorName.Text = "";
            }
            else
            {
                lblComfirmationText.Text = string.Format("Värde måste skrivas i båda fälten");
            }
        }

        protected void BtnToRace_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("race.aspx?race=" + raceSid);
        }
    }
}