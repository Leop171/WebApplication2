using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Datos;
using WebApplication1.Models;
using WebApplication1.Servicios;

namespace WebApplication1.Controllers
{
    public class InicioController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string nombre, string clave)
        {

            UsuarioDTO usuario = DBUsuario.Validar(nombre, UtilidadServicio.ConvertirSHA256(clave));
            if (usuario != null)
            {
                FormsAuthentication.SetAuthCookie(usuario.Nombre, false);
                return RedirectToAction("Registrar", "Encuesta");
            }
            else
            {
                ViewBag.Mensaje = "No se encontraron coincidencias";
            }

            return View();
        }


        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(UsuarioDTO usuario)
        {
            if (usuario.Clave != usuario.ConfirmarClave)
            {
                ViewBag.Nombre = usuario.Nombre;

                ViewBag.Mensaje = "Las contraseñas no coinciden";
                return View();
            }

            if (DBUsuario.Obtener(usuario.Nombre) == null)
            {
                usuario.Codigo = UtilidadServicio.GenerarCodigo(); // PK unica para cada usuario
                usuario.Clave = UtilidadServicio.ConvertirSHA256(usuario.Clave);
                usuario.Token = UtilidadServicio.GenerarToken();
                usuario.Fecha = DateTime.Now;

                bool respuesta = DBUsuario.Registrar(usuario);

                ViewBag.Mensaje = respuesta ? "Cuenta creada, puede iniciar session" : "No se pudo crear su cuenta";
            }
            else
            {
                ViewBag.Mensaje = "Nombre ya esta en uso";
            }


            return View();
        }

    }
}
