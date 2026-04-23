using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;
using System.Configuration;

namespace WebApplication1.Datos
{
    public class DBEncuesta
    {
        private static string CadenaSQL = ConfigurationManager.ConnectionStrings["DBACME"].ConnectionString;

        public static bool Registrar(UsuarioDTO usuario, EncuestaDTO encuesta)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
                {

                    string query = "insert into Encuesta (CodigoUsuario, Nombre, Descripcion, CamposCodigo, CodigoUnico) ";
                    query += " values(@codigoUsuario, @nombre,@descripcion, @CamposCodigo, @CodigoUnico)";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@codigoUsuario", "a06d953524c04331848b");

                    cmd.Parameters.AddWithValue("@nombre", encuesta.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", encuesta.Descripcion);
                    cmd.Parameters.AddWithValue("@CodigoUnico", encuesta.CodigoUnico);

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


        // Registrar encuesta
        public static int RegistrarEncuesta(string usuario, EncuestaDTO encuesta, string linkEncuesta)
        {
            int encuestaCodigo = 0;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
                {
                    oconexion.Open();

                    string query = "insert into Encuesta (CodigoUsuario, Nombre, Descripcion, CodigoUnico) OUTPUT INSERTED.Codigo ";
                    query += " values(@codigoUsuario, @nombre, @descripcion, @CodigoUnico)";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@codigoUsuario", usuario);
                    cmd.Parameters.AddWithValue("@nombre", encuesta.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", encuesta.Descripcion);
                    cmd.Parameters.AddWithValue("@CodigoUnico", linkEncuesta);

                    encuestaCodigo = (int)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return encuestaCodigo;
        }


        public static bool RegistrarPreguntas(int codigoEncuesta, List<PreguntaDTO> preguntas)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
                {
                    oconexion.Open();

                    foreach (var p in preguntas)
                    {
                        string queryDetalle = @"insert into Campo (CodigoEncuesta, Nombre, Titulo, Requerido, Tipo)
                                       VALUES (@CodigoEncuesta, @Nombre, @Titulo, @Requerido, @Tipo)";

                        SqlCommand cmdDetalle = new SqlCommand(queryDetalle, oconexion);

                        cmdDetalle.Parameters.AddWithValue("@CodigoEncuesta", codigoEncuesta);
                        cmdDetalle.Parameters.AddWithValue("@Nombre", p.Nombre);
                        cmdDetalle.Parameters.AddWithValue("@Titulo", p.Titulo);
                        cmdDetalle.Parameters.AddWithValue("@Requerido", p.Requerido);
                        cmdDetalle.Parameters.AddWithValue("@Tipo", p.tipo);

                        cmdDetalle.ExecuteNonQuery();
                    }

                    respuesta = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return respuesta;
        }


        public static bool Obtener(string enlace)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
                {
                    string query = "select CodigoUnico from Encuesta";
                    query += " where CodigoUnico = @enlace";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@enlace", enlace);
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


        // Encuesta por codigo Unico        
        public static bool ObtenerPorCodigo(string codigo)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
                {
                    string query = "select Codigo from Encuesta";
                    query += " where Codigo = @codigo";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@codigo", codigo);
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

        // Sobrecargado ya que la encuesta se puede editar en 2 vistas diferentes con diferente contexto
        public static EncuestaEditarDTO ObtenerPorCodigo(string codigo, string nombre)
        {
            EncuestaEditarDTO encuesta = null;
            string nuevoNombre = nombre + "nombre";
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
                {
                    string query = "select CodigoUnico, Nombre, Descripcion from Encuesta";
                    query += " where Codigo = @codigo";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            new EncuestaEditarDTO
                            {
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                CodigoUnico = dr["CodigoUnico"].ToString()
                            };

                        }
                    }
                }
                return encuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static bool ActualizarEncuesta(EncuestaEditarDTO encuesta, string codigo)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
            {
                string query = @"UPDATE Encuesta 
                         SET Nombre = @nombre, Descripcion = @descripcion
                         WHERE Codigo = @codigo";

                SqlCommand cmd = new SqlCommand(query, oconexion);

                cmd.Parameters.AddWithValue("@nombre", encuesta.Nombre);
                cmd.Parameters.AddWithValue("@descripcion", encuesta.Descripcion);
                cmd.Parameters.AddWithValue("@codigo", codigo);

                oconexion.Open();

                int filas = cmd.ExecuteNonQuery();
                respuesta = filas > 0;
            }

            return respuesta;
        }




        // Obtener la encuesta por codigo (Optimizar separar en 2 modelos)
        public static EncuestaVM ObtenerEncuesta(string codigo)
        {
            EncuestaVM encuesta = new EncuestaVM();
            encuesta.Preguntas = new List<PreguntasVM>();

            using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
            {
                oconexion.Open();

                string query = "select Codigo, CodigoUnico, Nombre, Descripcion from Encuesta";
                query += " where CodigoUnico = @enlace";

                SqlCommand cmd = new SqlCommand(query, oconexion);
                cmd.Parameters.AddWithValue("@enlace", codigo);
                string codigoPrueba = encuesta.CodigoUnico;
                string nombrePrueba = encuesta.Nombre;

                SqlDataReader dr = cmd.ExecuteReader();

                if (!dr.Read())
                    return null;

                encuesta.Codigo = Convert.ToInt32(dr["Codigo"]);
                encuesta.CodigoUnico = dr["CodigoUnico"].ToString();
                encuesta.Nombre = dr["Nombre"].ToString();
                encuesta.Descripcion = dr["Descripcion"].ToString();

                dr.Close();

                string queryDetalle = @"SELECT CodigoEncuesta, Nombre, Titulo, Requerido, Tipo 
                               FROM Campo 
                               WHERE CodigoEncuesta = @codigo";

                SqlCommand cmd2 = new SqlCommand(queryDetalle, oconexion);
                cmd2.Parameters.AddWithValue("@codigo", encuesta.Codigo);

                SqlDataReader dr2 = cmd2.ExecuteReader();

                while (dr2.Read())
                {
                    encuesta.Preguntas.Add(new PreguntasVM
                    {
                        CodigoEncuesta = Convert.ToInt32(dr2["CodigoEncuesta"]),
                        Nombre = dr2["Nombre"].ToString(),
                        Titulo = dr2["Titulo"].ToString(),
                        Tipo = dr2["Tipo"].ToString(),
                        Requerido = Convert.ToBoolean(dr2["Requerido"]),
                        Respuesta = "Texto"
                    });
                }
            }
            return encuesta;
        }


        public static bool Borrar(string codigo)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
            {

                oconexion.Open();
                SqlTransaction transaccion = oconexion.BeginTransaction();

                try
                {
                    string queryRespuesta = "delete from Respuesta ";
                    queryRespuesta += " where codigoEncuesta = @codigo";

                    SqlCommand cmdR = new SqlCommand(queryRespuesta, oconexion, transaccion);
                    cmdR.Parameters.AddWithValue("@codigo", codigo);
                    cmdR.ExecuteNonQuery();

                    string queryPregunta = "delete from Campo ";
                    queryPregunta += " where codigoEncuesta = @codigo";

                    SqlCommand cmdP = new SqlCommand(queryPregunta, oconexion, transaccion);
                    cmdP.Parameters.AddWithValue("@codigo", codigo);
                    cmdP.ExecuteNonQuery();


                    string queryEncuesta = "delete from Encuesta ";
                    queryEncuesta += " where codigo = @codigo";

                    SqlCommand cmdE = new SqlCommand(queryPregunta, oconexion, transaccion);
                    cmdE.Parameters.AddWithValue("@codigo", codigo);
                    cmdE.ExecuteNonQuery();

                    transaccion.Commit();
                    respuesta = true;

                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    throw ex;
                }
            }
            return respuesta;
        }

    }

}
