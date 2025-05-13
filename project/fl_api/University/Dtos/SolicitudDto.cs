using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Dtos
{
    public class SolicitudDto
    {
        public int? IdInsumo { get; set; }
        public int CantidadSolicitada { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Estado { get; set; } = null!;
        public string Observaciones { get; set; } = null!;
        public string NombreSolicitud { get; set; } = null!;
    }
}
