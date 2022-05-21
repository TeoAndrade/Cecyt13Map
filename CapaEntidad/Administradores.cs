using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Administradores
        // atributos de la clase ADMINISTRADORES, mismos que se mostraran en la tabla con el correspondiente nombre
    {
        public int Cve_Admin { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public bool Reestablecer { get; set; }

    }
}
