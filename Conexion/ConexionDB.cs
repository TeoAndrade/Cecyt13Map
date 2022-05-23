using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Conexion
{
    public class ConexionDB
    {
        //Se crea un elemento de tipo static para usar la cadena de conexion
        public static string cn = ConfigurationManager.ConnectionStrings["cadena"].ToString();
    }
}
