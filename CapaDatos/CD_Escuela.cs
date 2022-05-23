using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Conexion;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Escuela
    {
        //Es un metodo que elabora una lista de los salones con su piso y edificio. Realizando un inner join con la tabla Ubicacion
        public List<Escuela> Listar()
        {
            List<Escuela> cat = new List<Escuela>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConexionDB.cn))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("select e.Cve_Escuela,e.Nom_Escuela,e.Piso,u.Cve_Ubicacion,u.Nom_Ubicacion");
                    sb.AppendLine("from Escuela e");
                    sb.AppendLine("inner join Ubicacion u on e.Id_Ubicacion=u.Cve_Ubicacion");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), connection);
                    cmd.CommandType = CommandType.Text;
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cat.Add(new Escuela()
                            {
                                CveEscuela = Convert.ToInt32(reader["Cve_Escuela"]),
                                Nombre = reader["Nom_Escuela"].ToString(),
                                Piso = reader["Piso"].ToString(),
                                IDUbicacion = new Ubicacion()
                                {
                                    Cve_Ubicacion = Convert.ToInt32(reader["Cve_Ubicacion"]),
                                    Nom_Ubicacion = reader["Nom_Ubicacion"].ToString()
                                }
                            });
                        }
                    }
                }
            }
            catch
            {
                cat = new List<Escuela>();
            }

            return cat;
        }
        
        //Se registra un salon obteniendo un mensaje y resultado si es exitoso
        public int Registrar(Escuela obj, out string Mensaje)
        {
            int IdGuardado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConexionDB.cn))
                {
                    SqlCommand cmd = new SqlCommand("CrearSalon", connection);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Piso", obj.Piso);
                    cmd.Parameters.AddWithValue("@IdEscuela", obj.IDUbicacion.Cve_Ubicacion);
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    IdGuardado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                IdGuardado = 0;
                Mensaje = ex.Message;
            }
            return IdGuardado;
        }
        
        //Se editan los datos del salon anteriormente seleccionado
        public bool Editar(Escuela obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConexionDB.cn))
                {
                    SqlCommand cmd = new SqlCommand("EditarSalon", connection);
                    cmd.Parameters.AddWithValue("@CveEscuela", obj.CveEscuela);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Piso", obj.Piso);
                    cmd.Parameters.AddWithValue("@IdEscuela", obj.IDUbicacion.Cve_Ubicacion);
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }
            return resultado;
        }
        
        //Se elimina el salon previamente seleccionado
        public bool Eliminar(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConexionDB.cn))
                {
                    SqlCommand cmd = new SqlCommand("delete top(1) from Escuela where Cve_Escuela = @CveEscuela", connection);
                    cmd.Parameters.AddWithValue("@CveEscuela", id);
                    cmd.CommandType = CommandType.Text;

                    connection.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                        mensaje = "El salon no se pudo eliminar";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
