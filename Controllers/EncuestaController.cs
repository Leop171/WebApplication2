using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using WebApplication1.Datos;
using WebApplication1.Models;
using WebApplication1.Servicios;

namespace WebApplication1.Controllers
{

    public class EncuestaController : Controller
    {
        [Authorize]
        public ActionResult Registrar()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult Registrar(UsuarioDTO usuario, EncuestaDTO encuesta)
        {
            bool respuesta = DBEncuesta.Registrar(usuario, encuesta);

            ViewBag.Mensaje = respuesta ? "Su encuesta ha sido creada" : "No se pudo crear su encuesta";
            return View();
        }


        [Authorize]
        [HttpPost] // Encuesta cuando se presiona boton de registrar         
        public ActionResult RegistrarEncuesta(EncuestaDTO encuesta, string PreguntasJson)
        {
            bool respuesta = false;
            string link = UtilidadServicio.GenerarLink();

            string url = Request.Url.GetLeftPart(UriPartial.Authority);
            string codigo = DBUsuario.ObtenerPorNombre(User.Identity.Name);

            int encuestaCodigo = DBEncuesta.RegistrarEncuesta(codigo, encuesta, link);

            // Pueden existir encuestas sin preguntas
            if (PreguntasJson != null)
            {
                var preguntas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PreguntaDTO>>(PreguntasJson);
                respuesta = DBEncuesta.RegistrarPreguntas(encuestaCodigo, preguntas);
            }

            ViewBag.Mensaje = url + "/Encuesta/Responder/" + link;

            return View();

        }


        // Responder Encuestas
        public ActionResult Responder(string enlace, string accion, EncuestaVM encuestaTMP)
        {
            if (string.IsNullOrEmpty(enlace))
                return RedirectToAction("Login", "Inicio");

            var encuesta = DBEncuesta.ObtenerEncuesta(enlace);

            try
            {
                if (encuesta == null)
                    return HttpNotFound();

                if (accion == "enviar") // Formulario enviado, recolectar respuestas del usuario
                {
                    for (int i = 0; i < encuestaTMP.Preguntas.Count; i++)
                    {
                        int idPregunta = encuestaTMP.Preguntas[i].Codigo;
                        string respuesta = encuestaTMP.Preguntas[i].Respuesta;
                        encuesta.Preguntas[i].Respuesta = encuestaTMP.Preguntas[i].Respuesta;
                        encuesta.Preguntas[i].Codigo = encuestaTMP.Preguntas[i].Codigo;
                    }

                    string codigoRespuesta = DBPregunta.ObtenerPorEncuesta(encuesta.Codigo.ToString());

                    bool resultado = DBRespuesta.GuardarRespuestas(encuesta, codigoRespuesta);

                    ViewBag.Mensaje = "Respuesta Enviada";
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return View(encuesta);
        }


        // REVISAR
        public ActionResult ResponderEncuesta(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Index");

            var encuesta = DBEncuesta.ObtenerEncuesta(id);

            if (encuesta == null)
                return HttpNotFound();

            return View(encuesta);
        }

        [Authorize]
        // Borrar encuesta
        public ActionResult Borrar(string codigoEncuesta)
        {

            bool encuesta = false;

            encuesta = DBEncuesta.ObtenerPorCodigo(codigoEncuesta);
            if (encuesta == true)
            {
                DBEncuesta.Borrar(codigoEncuesta);
            }
            return View();
        }


        [Authorize]
        [HttpGet]
        public ActionResult Actualizar(string codigoEncuesta, string nombre)
        {
            var encuesta = DBEncuesta.ObtenerPorCodigo(codigoEncuesta, nombre);

            return View(encuesta);
        }


        [Authorize]
        [HttpPost]
        public ActionResult Actualizar(EncuestaEditarDTO encuesta, string accion, string codigoEncuesta)
        {
            // bool respuesta = DBEncuesta.ObtenerPorCodigo(codigoEncuesta);
            bool respuesta = false;

            if (accion == "enviar")
            {
                DBEncuesta.ActualizarEncuesta(encuesta, codigoEncuesta);
                respuesta = true;
            }
            ViewBag.Mensaje = respuesta ? "Encuesta actualizada correctamente" : "No se pudo actualizar la encuesta";

            return View(encuesta);
        }


        public ActionResult CerrarSession()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Inicio");
        }


        public ActionResult Test()
        {
            return Content(
            "Auth: " + User.Identity.IsAuthenticated +
            " | Name: " + User.Identity.Name
            );
        }


    }
}



