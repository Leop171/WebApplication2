using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Datos;
using WebApplication1.Models;
using WebApplication1.Servicios;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class RespuestaController : Controller
    {
        public ActionResult RegistrarRespuesta(EncuestaDTO encuesta, PreguntaDTO preguntas) // RespuestaDTO respuestas
        {
            bool respuesta = false;
            respuesta = DBRespuesta.Registrar(encuesta, preguntas);

            ViewBag.Mensaje = respuesta ? "Su encuesta ha sido creada prueba" : "Encuesta no creada, recargue la pagina e intente de nuevo";

            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult GuardarRespuestas(EncuestaVM model)
        {
            if (model == null || model.Preguntas == null)
                return RedirectToAction("Index");

            // Usarse como sustituto de RegistrarEncuesta
            bool resultado = DBRespuesta.GuardarRespuestas(model, "");

            if (resultado)
                return View("Gracias");

            return View("Error");
        }


    }
}
