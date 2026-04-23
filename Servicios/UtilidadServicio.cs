using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Servicios
{
    public static class UtilidadServicio
    {
        public static string ConvertirSHA256(string texto)
        {
            string hash = string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));

                foreach (byte b in hashValue)
                    hash += $"{b:X2}";

            }
            return hash;
        }

        public static string GenerarToken()
        {
            string token = Guid.NewGuid().ToString("N");
            return token;
        }


        public static string GenerarCodigo()
        {
            string id = Guid.NewGuid().ToString("N").Substring(0, 20);
            return id;

        }


        public static string GenerarLink()
        {
            string id = Guid.NewGuid().ToString("N").Substring(0, 20);

            return id;

        }

    }
}