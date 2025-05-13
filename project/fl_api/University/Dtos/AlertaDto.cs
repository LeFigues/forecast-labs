using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Dtos
{
    public class AlertaDto
    {
        public string Mensaje { get; set; } = null!;
        public string? Tipo { get; set; }
        public string Estado { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public string InsumoNombre { get; set; } = null!;
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public int StockMaximo { get; set; }
    }
}
