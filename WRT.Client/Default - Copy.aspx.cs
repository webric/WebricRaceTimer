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
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LaddaKontainrar();

                //Försök sätta vald kontainer
                CollectionHelper.SetSelectedByValue(ddlKontainrar, CacheHelper.GetSession("IPS_WhoAmI"));
            }
        }

        private void LaddaKontainrar()
        {
            var tracker = new TrackerService.TrackerServiceClient();
            var kontainrar = tracker.HämtaKontainrar();

            foreach (var kontainer in kontainrar)
            {
                var li = new ListItem
                {
                    Text = kontainer.Namn + " (" + kontainer.Serienummer + ")",
                    Value = kontainer.KontainerId.ToString(CultureInfo.InvariantCulture)
                };
                ddlKontainrar.Items.Add(li);
            }
        }

        protected void btnSkickaPing_OnClick(object sender, EventArgs e)
        {
            var tracker = new TrackerService.TrackerServiceClient();
            tracker.RegistreraKoordinater(kontainerId: int.Parse(ddlKontainrar.SelectedValue),
                                          tidpunkt: DateTime.Now,
                                          longitude: HämtaLongitude(),
                                          latitude: HämtaLatitude(),
                                          noggranhet: HämtaNoggranhet());
        }

        protected void btnStatusTom_OnClick(object sender, EventArgs e)
        {
            var tracker = new TrackerService.TrackerServiceClient();
            tracker.RegistreraKoordinaterOchStatus(kontainerId: int.Parse(ddlKontainrar.SelectedValue),
                                                   tidpunkt: DateTime.Now,
                                                   longitude: HämtaLongitude(),
                                                   latitude: HämtaLatitude(),
                                                   noggranhet: HämtaNoggranhet(),
                                                   status: "0");
        }

        protected void btnStatusHalv_OnClick(object sender, EventArgs e)
        {
            var tracker = new TrackerService.TrackerServiceClient();
            tracker.RegistreraKoordinaterOchStatus(kontainerId: int.Parse(ddlKontainrar.SelectedValue),
                                                   tidpunkt: DateTime.Now,
                                                   longitude: HämtaLongitude(),
                                                   latitude: HämtaLatitude(),
                                                   noggranhet: HämtaNoggranhet(),
                                                   status: "1");
        }

        protected void btnStatusFull_OnClick(object sender, EventArgs e)
        {
            var tracker = new TrackerService.TrackerServiceClient();
            tracker.RegistreraKoordinaterOchStatus(kontainerId: int.Parse(ddlKontainrar.SelectedValue),
                                                   tidpunkt: DateTime.Now,
                                                   longitude: HämtaLongitude(),
                                                   latitude: HämtaLatitude(),
                                                   noggranhet: HämtaNoggranhet(),
                                                   status: "2");
        }
        protected string HämtaLongitude()
        {
            var hid = (HiddenField)FindControl("ctl00$MainContent$hidLongitude");
            return hid != null ? hid.Value : "";
        }
        protected string HämtaLatitude()
        {
            var hid = (HiddenField)FindControl("ctl00$MainContent$hidLatutude");
            return hid != null ? hid.Value : "";
        }
        protected string HämtaNoggranhet()
        {
            var hid = (HiddenField)FindControl("ctl00$MainContent$hidNoggranhet");
            return hid != null ? hid.Value : "";
        }

        protected void btnStatusLost_OnClick(object sender, EventArgs e)
        {
            var tracker = new TrackerService.TrackerServiceClient();
            tracker.RegistreraKoordinaterOchStatus(kontainerId: int.Parse(ddlKontainrar.SelectedValue),
                                                   tidpunkt: DateTime.Now,
                                                   longitude: HämtaLongitude(),
                                                   latitude: HämtaLatitude(),
                                                   noggranhet: HämtaNoggranhet(),
                                                   status: "3");
        }

        protected void ddlKontainrar_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Util.CacheHelper.SetSession("IPS_WhoAmI", ddlKontainrar.SelectedValue);
        }
    }
}