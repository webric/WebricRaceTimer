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
    public partial class _NewRace : Page
    {
        private string RaceSid;

        protected void Page_Load(object sender, EventArgs e)
        {
            RaceSid = Request.QueryString["race"];

            if (!Page.IsPostBack)
            {
            }
        }

        protected void BtnSaveNewRace_OnClick(object sender, EventArgs e)
        {
            var timer = new TimerService.TimerServiceClient();
            RaceSid = timer.InitRace(txtRaceName.Text);

            Response.Redirect("race.aspx?race=" + RaceSid);
        }
    }
}