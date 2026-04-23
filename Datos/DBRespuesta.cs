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
    public class DBRespuesta
    {
        private static string CadenaSQL = ConfigurationManager.ConnectionStrings["DBACME"].ConnectionString;

        public static bool Registrar(EncuestaDTO encuesta, PreguntaDTO preguntas)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
                {

                    string query = "insert into Respuesta (CodigoEncuesta, CodigoCampos, Resultado) ";
                    query += " values(@codigoEncuesta, @codigoCampos, @Resultado)";

                    SqlCommand cmd = new SqlCommand(query, oconexion);

                    cmd.Parameters.AddWithValue("@codigoEncuesta", encuesta.Codigo);
                    cmd.Parameters.AddWithValue("@codigoCampos", preguntas.Codigo);
                    cmd.Parameters.AddWithValue("@Resultado", "Respuesta de la encuesta");

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





        // PRUBA INSERCION DE RESPUESTA

        public static bool GuardarRespuestas(EncuestaVM encuesta, string codigoRespuesta) //  PreguntasVM pregunta
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
            {
                oconexion.Open();

                try
                {
                    foreach (var p in encuesta.Preguntas)
                    {

                        string query = "insert into Respuesta (CodigoEncuesta, CodigoCampos, Resultado) ";
                        query += " values(@codigoEncuesta, @codigoCampos, @Resultado)";

                        SqlCommand cmd = new SqlCommand(query, oconexion);

                        cmd.Parameters.AddWithValue("@codigoEncuesta", p.CodigoEncuesta);
                        cmd.Parameters.AddWithValue("@codigoCampos", codigoRespuesta);
                        cmd.Parameters.AddWithValue("@Resultado", p.Respuesta);

                        cmd.ExecuteNonQuery();
                        respuesta = true;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return respuesta;
        }


    }
}

