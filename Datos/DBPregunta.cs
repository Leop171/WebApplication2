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
    public class DBPregunta
    {
        private static string CadenaSQL = ConfigurationManager.ConnectionStrings["DBACME"].ConnectionString;

        public static bool Obtener(EncuestaDTO encuesta)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
                {
                    string query = "Select CodigoEncuesta, CodigoCampos, Resultado from Campo ";
                    query += " where CodigoEncuesta = @CodigoEncuesta";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@CodigoEncuesta", encuesta.Codigo);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            respuesta = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return respuesta;
        }



        public static string ObtenerPorEncuesta(string codigo)
        {
            string respuestaCodigo = null;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
                {
                    string query = @"select Codigo from Campo " +
                        " where CodigoEncuesta = @CodigoEncuesta";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@CodigoEncuesta", codigo);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    var resultado = cmd.ExecuteScalar();

                    if (resultado != null)
                    {
                        respuestaCodigo = resultado.ToString();
                    }
                }
                return respuestaCodigo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
