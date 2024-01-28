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
    public partial class Default1 : System.Web.UI.Page
    {
        public List<Articulo> ProductosFiltrados { get; set; }
        public List<Articulo> ListaProductos { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ListaProductos = new List<Articulo>();
            try
            {

                ArticuloNegocio negocio = new ArticuloNegocio();
                ListaProductos = negocio.listar();
                if (txbFiltroDefault.Text == "" || ddwnFiltroCriterioDefault.Text == "" || ddwnFiltroCampoDefault.Text == "Filtrar por...")
                    btnBuscarFiltroDefault.Enabled = false;
                else
                    btnBuscarFiltroDefault.Enabled = true;

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }

        }

        protected void ddwnFiltroCampoDefault_SelectedIndexChanged(object sender, EventArgs e)
        {

            ddwnFiltroCriterioDefault.Items.Clear();

            if (ddwnFiltroCampoDefault.SelectedItem.ToString() == "Precio")
            {
                ddwnFiltroCriterioDefault.Items.Add("Mayor a:");
                ddwnFiltroCriterioDefault.Items.Add("Menor a:");
                ddwnFiltroCriterioDefault.Items.Add("Igual a:");
            }
            else
            {
                ddwnFiltroCriterioDefault.Items.Add("Contiene: ");
                ddwnFiltroCriterioDefault.Items.Add("Comienza con: ");
                ddwnFiltroCriterioDefault.Items.Add("Termina con: ");
            }

        }

        protected void btnBuscarFiltroDefault_Click(object sender, EventArgs e)
        {
            string valor = txbFiltroDefault.Text;
            string criterio = ddwnFiltroCriterioDefault.Text;
            string campo = ddwnFiltroCampoDefault.Text;


            ArticuloNegocio negocio = new ArticuloNegocio();
            ProductosFiltrados = negocio.listarFiltroDB(campo, criterio, valor);
            Session.Add("listaFiltrada", ProductosFiltrados);
            Response.Redirect("FiltroDefault.aspx");
        }
    }
}