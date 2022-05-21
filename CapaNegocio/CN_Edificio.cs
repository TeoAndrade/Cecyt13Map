using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;

using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Edificio
        // En las clases CapaDatos, CapaEntidad, se mostraran la tabla Edificion en privado 
        //La tabla Edificio mostrara un listado de ubicaciones  
    {
        private CD_Ubicacion edificio = new CD_Ubicacion();

        public List<Ubicacion> Listar()
        {
            return edificio.Listar();
        }
        public int Registrar(Ubicacion obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nom_Ubicacion) || string.IsNullOrWhiteSpace(obj.Nom_Ubicacion))
            {
                Mensaje = "El nombre del edificio no puede ser vacio";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return edificio.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }


        }
        public bool Editar(Ubicacion obj, out string mensaje)
            // Editar nombre de los edificios 
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nom_Ubicacion) || string.IsNullOrWhiteSpace(obj.Nom_Ubicacion))
            {
                mensaje = "El nombre del edificio no puede ser vacio";
            }


            if (string.IsNullOrEmpty(mensaje))
                return edificio.Editar(obj, out mensaje);
            else
                return false;
        }
        public bool Eliminar(int id, out string mensaje)
        {
            return edificio.Eliminar(id, out mensaje);
        }
    }
}
