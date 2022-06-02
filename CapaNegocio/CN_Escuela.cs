﻿using System;
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
        //Se crea un elemento de la clase CD_Escuela para acceder a sus metodos
        private CD_Escuela objEscuela = new CD_Escuela();
        
        //Se accede a la lista de salones creada
        public List<Escuela> Listar()
        {
            return objEscuela.Listar();
        }
        
        //Se realizan las validaciones para registrar al salon
        public int Registrar(Escuela obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del salon no puede ser vacio";
                if (string.IsNullOrEmpty(obj.Piso) || string.IsNullOrWhiteSpace(obj.Piso) || obj.Piso.Length > 2)
                {
                    Mensaje = "El nombre y piso del salon no puedenser vacios";
                    if (obj.IDUbicacion.Cve_Ubicacion == 0)
                    {
                        Mensaje = "El nombre, piso y edificio del salon no puedenser vacios o cero";
                    }
                }
                else if (obj.IDUbicacion.Cve_Ubicacion == 0)
                {
                    Mensaje = "El nombre y edificio del salon no puedenser vacios o cero";
                }
            }
            else if (string.IsNullOrEmpty(obj.Piso) || string.IsNullOrWhiteSpace(obj.Piso) || obj.Piso.Length>2)
            {
                Mensaje = "El piso del salon no puede ser vacio o mayor a dos caracteres";
                if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
                {
                    Mensaje = "El nombre y piso del salon no puedenser vacios";
                    if (obj.IDUbicacion.Cve_Ubicacion == 0)
                    {
                        Mensaje = "El nombre, piso y edificio del salon no puedenser vacios";
                    }
                }
                else if (obj.IDUbicacion.Cve_Ubicacion == 0)
                {
                    Mensaje = "El piso y edificio del salon no puedenser vacios";
                }
            }
            else if (obj.IDUbicacion.Cve_Ubicacion == 0)
            {
                Mensaje = "El edificio del salon no puede ser vacio";
                if (string.IsNullOrEmpty(obj.Piso) || string.IsNullOrWhiteSpace(obj.Piso) || obj.Piso.Length > 2)
                {
                    Mensaje = "El edificio y piso (no mayor a 2 caracteres) del salon no pueden ser vacios";
                    if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
                    {
                        Mensaje = "El nombre, piso (no mayor a 2 caracteres) y edificio del salon no pueden ser vacios";
                    }
                }
                else if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
                {
                    Mensaje = "El nombre y edificio del salon no pueden ser vacios";
                }
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
        
        //Se realizan las mismas validaciones y si las pasa lo actualiza
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

        //Se accede al metodo para poder eliminar el salon
        public bool Eliminar(int id, out string mensaje)
        {
            return objEscuela.Eliminar(id, out mensaje);
        }
    }
}
