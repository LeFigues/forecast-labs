using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Dtos
{
    public class SolicitudUsoDto
    {
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFin { get; set; }
        public int NumeroEstudiantes { get; set; }
        public int TamanoGrupo { get; set; }
        public int NumeroGrupos { get; set; }
        public string Estado { get; set; } = null!;
        public string Observaciones { get; set; } = null!;
        public string DocenteNombre { get; set; } = null!;
        public string PracticaTitulo { get; set; } = null!;
        public string LaboratorioNombre { get; set; } = null!;
    }
}
