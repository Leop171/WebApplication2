using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Datos;

namespace WebApplication1.Controllers
{
    public class ResultadosController : Controller
    {
        [Authorize]
        public ActionResult Obtener()
        {
            string usuario = User.Identity.Name;

            var encuestas = DBResultado.ObtenerPorUsuario(usuario);

            return View(encuestas);
        }
    }
}

