using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace WebApplication1.Models
{
    public class EncuestaDTO
    {
        public int Codigo { get; set; }

        public string CodigoUsuario { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string CodigoUnico { get; set; }

        public DateTime FechaCreacion { get; set; }


    }
}