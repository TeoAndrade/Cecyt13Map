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
    {
        //Se crea un elemento de la clase CD_Ubicacion para hacer llamado a los metodos
        private CD_Ubicacion edificio = new CD_Ubicacion();
        
        //Se crea un metodo para poder acceder a lista creada
        public List<Ubicacion> Listar()
        {
            return edificio.Listar();
        }
        
        //Se crea un salon solo si pasa las validaciones 
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
        
        //Se realiza la misma validacion 
        public bool Editar(Ubicacion obj, out string mensaje)
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
        
        //Se accede al metodo eliminar de la clase CD_Ubicacion
        public bool Eliminar(int id, out string mensaje)
        {
            return edificio.Eliminar(id, out mensaje);
        }
    }
}
