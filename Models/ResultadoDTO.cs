using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ResultadoDTO
    {
        public int Codigo { get; set; }
        public string CodigoUnico { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string CodigoEncuesta { get; set; } // Codigo resultado != CodigoEncuesta 
        public string NombreCampos { get; set; }
        public string TituloCampos { get; set; }
        public string NombreEncuesta { get; set; }
        public string Resultado { get; set; }

    }
}
