using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Datos
{
    public class DBUsuario
    {
        private static string CadenaSQL = ConfigurationManager.ConnectionStrings["DBACME"].ConnectionString;

        public static bool Registrar(UsuarioDTO usuario)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
                {

                    string query = "insert into Usuario(Codigo, Nombre,Clave, Fecha,Token)";
                    query += " values(@codigo, @nombre,@clave, @Fecha, @token)";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@codigo", usuario.Codigo); // Codigo generado viene de DBUsuario 
                    cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@clave", usuario.Clave);
                    cmd.Parameters.AddWithValue("@fecha", usuario.Fecha);
                    cmd.Parameters.AddWithValue("@token", usuario.Token);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) respuesta = true;
                }

                return respuesta;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static UsuarioDTO Validar(string nombre, string clave)

        {
            UsuarioDTO usuario = null;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
                {
                    string query = "select Nombre from Usuario";
                    query += " where Nombre=@nombre and Clave = @clave";



                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            usuario = new UsuarioDTO()
                            {
                                Nombre = dr["Nombre"].ToString(),
                            };
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return usuario;
        }



        public static UsuarioDTO Obtener(string nombre)
        {
            UsuarioDTO usuario = null;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
                {
                    string query = "select Nombre,Clave,Fecha,Token from Usuario";
                    query += " where Nombre=@nombre";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            usuario = new UsuarioDTO()
                            {
                                Nombre = dr["Nombre"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Fecha = (DateTime)dr["Fecha"],
                                Token = dr["Token"].ToString()
                            };
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return usuario;
        }


        // OBTENER POR USUARIO
        public static string ObtenerPorNombre(string nombre)
        {
            string resultadoCodigo = null;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
                {
                    string query = @"select Codigo from Usuario " +
                        " where nombre = @nombre";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    var resultado = cmd.ExecuteScalar();

                    if (resultado != null)
                    {
                        resultadoCodigo = resultado.ToString();
                    }
                }

                return resultadoCodigo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}