﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_2184587.Models
{
    public class Reporte
    {
        public string nombreProveedor { get; set; }
        public string direccionProveedor { get; set; }
        public string telefonoProveedor { get; set; }
        public string nombreProducto { get; set; }
        public int? precioProducto{ get; set; }

    }
}