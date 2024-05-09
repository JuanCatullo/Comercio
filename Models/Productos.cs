using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comercio.Models
{
    public class Productos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaCarga { get; set; }
        public int CategoriaId { get; set; }
    }
}