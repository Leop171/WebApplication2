using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Datos
{
    public class DBResultado
    {
        private static string CadenaSQL = ConfigurationManager.ConnectionStrings["DBACME"].ConnectionString;

        public static List<ResultadoDTO> ObtenerPorUsuario(string usuario)
        {
            List<ResultadoDTO> lista = new List<ResultadoDTO>();

            using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
            {
                string query = @"SELECT U.Codigo AS UsuarioCodigo, U.Nombre AS UsuarioNombre,
                                E.Codigo AS EncuestaCodigo, E.Nombre AS NombreEncuesta, E.CodigoUnico, E.FechaCreacion,
                                C.Codigo AS CampoCodigo, C.CodigoEncuesta, C.Titulo, C.Nombre AS CampoNombre,
                                R.Resultado
                                FROM Encuesta E
                                INNER JOIN Campo C ON E.Codigo = C.CodigoEncuesta
                                LEFT JOIN Respuesta R ON C.Codigo = R.CodigoCampos                                
                                INNER JOIN Usuario U ON U.Codigo = E.CodigoUsuario
                                WHERE U.Nombre = @usuario";

                SqlCommand cmd = new SqlCommand(query, oconexion);
                cmd.Parameters.AddWithValue("@usuario", usuario);

                oconexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new ResultadoDTO
                        {
                            Codigo = Convert.ToInt32(dr["EncuestaCodigo"]),
                            CodigoUnico = dr["CodigoUnico"].ToString(),
                            CodigoEncuesta = dr["CodigoEncuesta"].ToString(),
                            NombreEncuesta = dr["NombreEncuesta"].ToString(),
                            TituloCampos = dr["Titulo"].ToString(),
                            NombreCampos = dr["CampoNombre"].ToString(),
                            Resultado = dr["Resultado"].ToString()

                        });
                    }
                }
            }

            return lista;
        }
    }
}