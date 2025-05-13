using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Dtos
{
    public class MovimientoInventarioDto
    {
        public string TipoMovimiento { get; set; } = null!;
        public DateTime FechaEntregado { get; set; }
        public DateTime? FechaDevuelto { get; set; }
        public int Cantidad { get; set; }
        public string Responsable { get; set; } = null!;
        public int IdSolicitud { get; set; }
        public string InsumoNombre { get; set; } = null!;
    }
}
