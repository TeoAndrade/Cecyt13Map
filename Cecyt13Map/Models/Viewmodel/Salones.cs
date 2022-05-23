using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cecyt13Map.Models.Viewmodel
{
    //Propiedades de la clase salones
    public class Salones
    {
        public int Cve_Salon { get; set; }
        public string Nom_salon { get; set; }
        public string Piso { get; set; }
        public UbicacionViewModel IdUbi { get; set; }
    }
}
