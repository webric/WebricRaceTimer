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
    public partial class _StartRace : Page
    {
        private string raceSid;

        protected void Page_Load(object sender, EventArgs e)
        {
            raceSid = Request.QueryString["race"];

            if (raceSid == "" || raceSid == null)
                Response.Redirect("default.aspx");
        }

        protected void BtnStartRace_OnClick(object sender, EventArgs e)
        {
            var timer = new TimerService.TimerServiceClient();
            timer.StartRace(raceSid);

            Response.Redirect("race.aspx?race=" + raceSid);
        }
    }
}