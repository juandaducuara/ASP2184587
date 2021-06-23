using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_2184587.Models
{
    public class Facturacion
    {
        public string nombreCliente { get; set; }
        public string documentoCliente { get; set; }
        public DateTime? fechaCompra { get; set; }
        public int? totalCompra { get; set; }        
    }
}