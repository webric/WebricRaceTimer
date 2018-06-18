using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WRT.Core.BLL;

namespace WRT.Monitor
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                LaddaKontainrar();

            LaddaPositionerna();
        }

        private void LaddaKontainrar()
        {
            //var kon = new Kontainer();
            //var kontainrar = kon.Hämta();

            ////Lägg till alla
            //var liAlla = new ListItem
            //{
            //    Text = "Alla",
            //    Value = ""
            //};
            //ddlKontainrar.Items.Add(liAlla);

            //foreach (var kontainer in kontainrar)
            //{
            //    var li = new ListItem
            //    {
            //        Text = kontainer.Namn + " (" + kontainer.Serienummer + ")",
            //        Value = kontainer.KontainerId.ToString(CultureInfo.InvariantCulture)
            //    };
            //    ddlKontainrar.Items.Add(li);
            //}
        }

        private void LaddaPositionerna()
        {
            ////Kontrollera datumintervall
            //DateTime? from = null;
            //if (txtFrom.Text != "")
            //    from = DateTime.Parse(txtFrom.Text);

            //DateTime? tom = null;
            //if (txtTom.Text != "")
            //    tom = DateTime.Parse(txtTom.Text);

            //var pos = new Position();
            //DataTable positioner;

            ////Kontrollera vilka kontainrar
            //if (ddlKontainrar.SelectedValue == "")
            //    positioner = pos.HämtaTillLista(null, from, tom);
            //else
            //    positioner = pos.HämtaTillLista(int.Parse(ddlKontainrar.SelectedValue), from, tom);

            //rpSenastePositionerna.DataSource = positioner;
            //rpSenastePositionerna.DataBind();

            //rptMarkers.DataSource = positioner;
            //rptMarkers.DataBind();

            ////Populera gömdaKoordinatefältet
            //var builder = new StringBuilder();
            //foreach (DataRow position in positioner.Rows)
            //{
            //    builder.Append(position["Longitude"] + ":" + position["Longitude"] + ";");
            //}
            //hidKoordinater.Value = builder.ToString();
        }

        protected void ddlKontainrar_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            LaddaPositionerna();
        }

        protected void txtTom_OnTextChanged(object sender, EventArgs e)
        {
            LaddaPositionerna();
        }

        protected void txtFrom_OnTextChanged(object sender, EventArgs e)
        {
            LaddaPositionerna();
        }

        protected void btnSök_OnClick(object sender, EventArgs e)
        {
            LaddaPositionerna();
        }
    }
}