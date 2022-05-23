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
    public class CD_Ubicacion
    {
        //Se crea una lisda de los edificios de la escuela
        public List<Ubicacion> Listar()
        {
            List<Ubicacion> cat = new List<Ubicacion>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConexionDB.cn))
                {
                    string query = "select Cve_Ubicacion,Nom_Ubicacion from Ubicacion";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.CommandType = CommandType.Text;
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cat.Add(new Ubicacion()
                            {
                                Cve_Ubicacion = Convert.ToInt32(reader["Cve_Ubicacion"]),
                                Nom_Ubicacion = reader["Nom_Ubicacion"].ToString()
                            });
                        }
                    }
                }
            }
            catch
            {
                cat = new List<Ubicacion>();
            }

            return cat;
        }
        
        //Se crea un nuevo edificio con el dato indicado previamente, solo si no exite uno con el mismo nombre
        public int Registrar(Ubicacion obj, out string Mensaje)
        {
            int IdGuardado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConexionDB.cn))
                {
                    SqlCommand cmd = new SqlCommand("CrearEdificio", connection);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nom_Ubicacion);
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
        
        //Se editan los datos del edificio seleccionado
        public bool Editar(Ubicacion obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConexionDB.cn))
                {
                    SqlCommand cmd = new SqlCommand("EditarEdificio", connection);
                    cmd.Parameters.AddWithValue("@IdEdificio", obj.Cve_Ubicacion);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nom_Ubicacion);
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
        
        //Se elimina el edificio seleccionado solo si no esta relacionado a un salon
        public bool Eliminar(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConexionDB.cn))
                {
                    SqlCommand cmd = new SqlCommand("EliminarEdificio", connection);
                    cmd.Parameters.AddWithValue("@IdEdificio", id);
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
    }
}
