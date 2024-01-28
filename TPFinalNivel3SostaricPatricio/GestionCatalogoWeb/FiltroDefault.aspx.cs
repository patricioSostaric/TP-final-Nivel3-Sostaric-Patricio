using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace GestionCatalogoWeb
{
    public partial class FiltroDefault : System.Web.UI.Page
    {
        public List<Articulo> MostrarFiltro { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                MostrarFiltro = (List<Articulo>)Session["listaFiltrada"];
            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnVolverFiltroDefault_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx",false);
        }
    }
}