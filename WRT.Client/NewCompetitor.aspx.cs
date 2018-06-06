using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WRT.Core.BLL;
using WRT.Core.Util;

namespace WRT.Client
{
    public partial class _NewCompetitor : Page
    {
        private string RaceSid;

        protected void Page_Load(object sender, EventArgs e)
        {
            RaceSid = Request.QueryString["race"];

            if (!Page.IsPostBack)
            {
            }
        }

        protected void BtnToRace_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("race.aspx?race=" + RaceSid);
        }

        protected void BtnSaveNewCompetitor_OnClick(object sender, EventArgs e)
        {
            var timer = new TimerService.TimerServiceClient();
            var competitor = timer.CreateCompetitor(txtCompetitorNumber.Text, txtCompetitorName.Text);

            lblComfirmationText.Text = string.Format("{0}, {1} tillagd", competitor.Number, competitor.Name);
            txtCompetitorNumber.Text = "";
            txtCompetitorName.Text = "";
        }
    }
}