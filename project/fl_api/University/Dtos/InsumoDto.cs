using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Dtos
{
    public class InsumoDto
    {
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Ubicacion { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public string UnidadMedida { get; set; } = null!;
        public int StockActual { get; set; }
    }
}
