using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Escuela
        //Dentro de la clase CapaEntidad, se mostrara la tabla escuela con los datos correspondiente
    {
        public int CveEscuela { get; set; }
        public string Nombre { get; set; }
        public string Piso { get; set; }
        public Ubicacion IDUbicacion { get; set; }
    }
}
