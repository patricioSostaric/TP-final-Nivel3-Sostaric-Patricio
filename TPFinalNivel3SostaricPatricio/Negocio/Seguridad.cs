using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
     public class Seguridad
    {
        public static bool sesionActiva(object user)
        {
            User trainee = user != null ? (User)user : null;
            if (trainee != null && trainee.Id != 0)
                return true;
            else
                return false;
        }

        public static bool esAdmin(object user)
        {
            User trainee = user != null ? (User)user : null;
            return trainee != null ? trainee.Admin : false;
        }
    }
}
