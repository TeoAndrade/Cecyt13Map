using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;

namespace Cecyt13Map.Models
{
    public class Recursos
    {
        // clase recursos, que ayuda a mostrar los listados 
        public List<Usuario> Listar()
        {
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                using(SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-BC85JKD\SQL;Initial Catalog=Cecyt13Map;Integrated Security=True"))
                {
                    string sql = "select Cve_Usuario,Nom_Usuario,Correo,Contraseña from Usuario";
                    SqlCommand cmd = new SqlCommand(sql,con);
                    cmd.CommandType=CommandType.Text;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(new Usuario()
                            {
                                //lista de usuarios existentes
                                Cve_Usuario = Convert.ToInt32(reader["Cve_Usuario"]),
                                Nom_Usuario=reader["Nom_Usuario"].ToString(),
                                Correo=reader["Correo"].ToString(),
                                Contraseña=reader["Contraseña"].ToString()
                            });
                        }
                    }
                }
            }
            catch
            {
                usuarios = new List<Usuario>();
            }
            return usuarios;
        }
    }
}
