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
    public partial class _Race : Page
    {
        private string RaceSid;

        protected void Page_Load(object sender, EventArgs e)
        {
            RaceSid = Request.QueryString["race"];

            if (!Page.IsPostBack)
            {
            }
        }

        protected void BtnAddCompetitor_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("/NewCompetitor.aspx");
        }
    }
}