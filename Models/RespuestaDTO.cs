using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class RespuestaDTO
    {
        public int Codigo { get; set; }
        public int CodigoEncuesta { get; set; }
        public int CodigoCampos { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Resultado { get; set; }

    }
}
