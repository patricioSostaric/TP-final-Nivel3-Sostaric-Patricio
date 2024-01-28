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
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (!Page.IsValid)
                    return;

                User user = new User();
                UserNegocio traineeNegocio = new UserNegocio();
                //EmailService emailService = new EmailService();

                user.Email = txtEmail.Text;
                user.Pass = txtPassword.Text;
                user.Id = traineeNegocio.insertarNuevo(user);
                Session.Add("trainee", user);

                //emailService.armarCorreo(user.Email, "Bienvenido/a", "Te damos la bienvenida a nuestra web de articulos");
                //emailService.enviarEmail();
                Response.Redirect("Default.aspx", false);

            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}