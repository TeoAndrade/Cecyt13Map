using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;

using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Escuela
    {
        private CD_Escuela objEscuela = new CD_Escuela();

        public List<Escuela> Listar()
        {
            return objEscuela.Listar();
        }
        public int Registrar(Escuela obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre de la escuela no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Piso) || string.IsNullOrWhiteSpace(obj.Piso))
            {
                Mensaje = "El piso de la escuela no puede ser vacio";
            }
            else if (obj.IDUbicacion.Cve_Ubicacion == 0)
            {
                Mensaje = "Debe seleccionar un edificio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objEscuela.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }


        }
        public bool Editar(Escuela obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                mensaje = "El nombre de la escuela no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Piso) || string.IsNullOrWhiteSpace(obj.Piso))
            {
                mensaje = "El piso de la escuela no puede ser vacio";
            }
            else if (obj.IDUbicacion.Cve_Ubicacion == 0)
            {
                mensaje = "Debe seleccionar un edificio";
            }

            if (string.IsNullOrEmpty(mensaje))
                return objEscuela.Editar(obj, out mensaje);
            else
                return false;
        }

        public bool Eliminar(int id, out string mensaje)
        {
            return objEscuela.Eliminar(id, out mensaje);
        }
    }
}
