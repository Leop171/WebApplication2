using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class UsuarioDTO
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public string ConfirmarClave { get; set; }
        public DateTime Fecha { get; set; }
        public string Token { get; set; }

    }
}

