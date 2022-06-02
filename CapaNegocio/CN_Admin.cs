using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Admin
    {
        //Se crea un elemento de tipo privado para poder acceder a los metodos de la clase CD_Admin
        private CD_Admin admin = new CD_Admin();
        
        //Se crea un metodo para mandar a llamar de la clase CD_Admin la funcion Listar
        public List<Administradores> Listar()
        {
            return admin.Listar();
        }
        
        //Se realizan algunas validaciones de los datos ingresados por el administrador para evitar errores al guardar
        public int Registrar(Administradores obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                mensaje = "El nombre del administrador no puede ser vacio";
                if (string.IsNullOrEmpty(obj.Apellido) || string.IsNullOrWhiteSpace(obj.Apellido))
                {
                    mensaje = "El nombre y apellido del administrador no puedenser vacios";
                    if(string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
                    {
                        mensaje = "El nombre, apellido y correo del administrador no puedenser vacios";
                    }
                }
                else if(string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
                {
                    mensaje = "El nombre y correo del administrador no puedenser vacios";
                }
            }
            else if (string.IsNullOrEmpty(obj.Apellido) || string.IsNullOrWhiteSpace(obj.Apellido))
            {
                mensaje = "El apellido del administrador no puede ser vacio";
                if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
                {
                    mensaje = "El nombre y apellido del administrador no puedenser vacios";
                    if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
                    {
                        mensaje = "El nombre, apellido y correo del administrador no puedenser vacios";
                    }
                }
                else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
                {
                    mensaje = "El apellido y correo del administrador no puedenser vacios";
                }
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                mensaje = "El correo del administrador no puede ser vacio";
                if (string.IsNullOrEmpty(obj.Apellido) || string.IsNullOrWhiteSpace(obj.Apellido))
                {
                    mensaje = "El correo y apellido del administrador no pueden ser vacios";
                    if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
                    {
                        mensaje = "El nombre, apellido y correo del administrador no pueden ser vacios";
                    }
                }
                else if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
                {
                    mensaje = "El nombre y correo del administrador no pueden ser vacios";
                }
            }
            if (string.IsNullOrEmpty(mensaje))
            {
                string clave = CN_Recursos.GenerarClave();
                string asunto = "Creacion de cuenta";
                string Mensaje = "<h3>Su cuenta fue creada correctamente</h3></br><p>Su contraseña es:!clave¡</p>";
                Mensaje = Mensaje.Replace("!clave¡", clave);

                bool res = CN_Recursos.EnviarCorreo(obj.Correo, asunto, Mensaje);
                if (res)
                {
                    obj.Contraseña = CN_Recursos.Encriptar(clave);
                    return admin.Registrar(obj, out Mensaje);
                }
                else
                {
                    Mensaje = "No se puede enviar el correo";
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        
        //Se relizan las mismas validaciones para evitar errores a la hora de actualizar datos
        public bool Editar(Administradores obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                mensaje = "El nombre del administrador no puede ser vacio";
                if (string.IsNullOrEmpty(obj.Apellido) || string.IsNullOrWhiteSpace(obj.Apellido))
                {
                    mensaje = "El nombre y apellido del administrador no puedenser vacios";
                    if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
                    {
                        mensaje = "El nombre, apellido y correo del administrador no puedenser vacios";
                    }
                }
                else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
                {
                    mensaje = "El nombre y correo del administrador no puedenser vacios";
                }
            }
            else if (string.IsNullOrEmpty(obj.Apellido) || string.IsNullOrWhiteSpace(obj.Apellido))
            {
                mensaje = "El apellido del administrador no puede ser vacio";
                if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
                {
                    mensaje = "El nombre y apellido del administrador no puedenser vacios";
                    if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
                    {
                        mensaje = "El nombre, apellido y correo del administrador no puedenser vacios";
                    }
                }
                else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
                {
                    mensaje = "El apellido y correo del administrador no puedenser vacios";
                }
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                mensaje = "El correo del administrador no puede ser vacio";
                if (string.IsNullOrEmpty(obj.Apellido) || string.IsNullOrWhiteSpace(obj.Apellido))
                {
                    mensaje = "El correo y apellido del administrador no puedenser vacios";
                    if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Correo))
                    {
                        mensaje = "El nombre, apellido y correo del administrador no puedenser vacios";
                    }
                }
                else if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Correo))
                {
                    mensaje = "El nombre y correo del administrador no puedenser vacios";
                }
            }

            if (string.IsNullOrEmpty(mensaje))
                return admin.Editar(obj, out mensaje);
            else
                return false;
        }
        
        //Se hace el llamado de metodo Eliminar y se envian los datos requeridos por el mismo
        public bool Eliminar(int id, out string mensaje)
        {
            return admin.Eliminar(id, out mensaje);
        }
        
        //Se cambia la clave del administrador por una que el disigne
        public bool CambiarClave(int id, string nueva, out string mensaje)
        {
            return admin.CambiarClave(id, nueva, out mensaje);
        }
        
        //Se le envia un correo al administrador con la contraseña generada por el sistema que posteriormente la cambiara
        public bool Reestablecer(int id, string correo, out string mensaje)
        {
            mensaje = string.Empty;
            string newclave = CN_Recursos.GenerarClave();
            bool res = admin.Reestablecer(id, CN_Recursos.Encriptar(newclave), out mensaje);

            if (res)
            {
                string asunto = "Contraseña reestablecida";
                string Mensaje = "<h3>Su contraseña fue reestablecida correctamente</h3></br><p>Su nueva contraseña es:!clave¡</p>";
                Mensaje = Mensaje.Replace("!clave¡", newclave);

                bool respuesta = CN_Recursos.EnviarCorreo(correo, asunto, Mensaje);
                if (res)
                {
                    return true;
                }
                else
                {
                    mensaje = "No se pudo enviar el correo";
                    return false;
                }
            }
            else
            {
                mensaje = "No se pudo reestablecer la contraseña";
                return false;
            }
        }
    }
}
