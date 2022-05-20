using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cecyt13Map.Models
{
    public class Usuario
    {
        public int Cve_Usuario { get; set; }
        public string Nom_Usuario { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public string ConfirmarContra { get; set; }
    }
}