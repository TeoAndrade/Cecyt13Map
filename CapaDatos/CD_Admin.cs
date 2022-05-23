using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Conexion;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Admin
    {
        public List<Administradores> Listar()
            //Creamos nuevo listado en el apartado de Administradores
        {
            List<Administradores> lista = new List<Administradores>();
            try
            {
                using(SqlConnection con=new SqlConnection(ConexionDB.cn))
                {
                    string query = "select CveAdmin,Nom_Admin,Apellido,CorreoAdmi,Clave,Restablecer from Administradores";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Administradores()
                            {
                                Cve_Admin = Convert.ToInt32(reader["CveAdmin"]),
                                Nombre = reader["Nom_Admin"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                Correo = reader["CorreoAdmi"].ToString(),
                                Contraseña = reader["Clave"].ToString(),
                                Reestablecer=Convert.ToBoolean( reader["Restablecer"])
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Administradores>();
            }
            return lista;
        }
        public int Registrar(Administradores obj,out string mensaje)
            // El nuevo Administrador debe registrase con  los datos previamente insertados
        {
            int Res=0;
            mensaje=string.Empty;
            try
            {
                using(SqlConnection conn=new SqlConnection(ConexionDB.cn))
                {
                    SqlCommand cmd = new SqlCommand("RegistrarAdmin",conn);
                    cmd.Parameters.AddWithValue("@Nombre",obj.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", obj.Apellido);
                    cmd.Parameters.AddWithValue("@Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("@Clave", obj.Contraseña);
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    Res=Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    mensaje=cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch(Exception ex)
            {
                Res=0;
                mensaje=ex.Message;
            }
            return Res;
        }
        public bool Editar(Administradores obj, out string mensaje)
        {
            bool Res = false;
            mensaje =string.Empty;
            try
            {
                using(SqlConnection conn = new SqlConnection(ConexionDB.cn))
                {
                    SqlCommand cmd = new SqlCommand("EditarAdmin",conn);
                    cmd.Parameters.AddWithValue("@Cve_Admin",obj.Cve_Admin);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", obj.Apellido);
                    cmd.Parameters.AddWithValue("@Correo", obj.Correo);
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    Res=Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    mensaje=cmd.Parameters["Mensaje"].Value.ToString();
                }
            }catch(Exception ex)
            {
                Res=false;
                mensaje=ex.Message;
            }
            return Res;
        }
        public bool Eliminar(int id,out string mensaje)
        {
            bool res=false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConexionDB.cn))
                {
                    SqlCommand cmd=new SqlCommand("delete top(1) from Administradores where CveAdmin = @Cve_Admin",conn);
                    cmd.Parameters.AddWithValue("@Cve_Admin", id);
                    cmd.CommandType=CommandType.Text;
                    conn.Open();
                    res=cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch(Exception ex)
            {
                res=false;
                mensaje = ex.Message;
            }
            return res;
        }
        public bool CambiarClave(int id,string nuevaClave, out string mensaje)
            //en caso de que el usuario es nuevo o se le olvida la contraseñ, puede crearse  una nueva
        {
            bool res = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConexionDB.cn))
                {
                    SqlCommand cmd = new SqlCommand("update Administradores set Clave=@clave,Restablecer=0 where CveAdmin=@CveAdmin", conn);
                    cmd.Parameters.AddWithValue("@CveAdmin", id);
                    cmd.Parameters.AddWithValue("@clave", nuevaClave);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    res = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                res = false;
                mensaje = ex.Message;
            }
            return res;
        }
        public bool Reestablecer(int id,string clave, out string mensaje)
            //Reestablecer contraseña en donde se envia atravez del correo del usuario y la clave se genera por el sistema
        {
            bool res = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConexionDB.cn))
                {
                    SqlCommand cmd = new SqlCommand("update Administradores set Clave=@clave,Restablecer=1 where CveAdmin=@CveAdmin", conn);
                    cmd.Parameters.AddWithValue("@CveAdmin", id);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    res = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                res = false;
                mensaje = ex.Message;
            }
            return res;
        }
    }
}
