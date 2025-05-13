using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Dtos
{
    public class LaboratorioDto
    {
        public string Nombre { get; set; } = null!;
        public string? Ubicacion { get; set; }
        public string? Descripcion { get; set; }
        public int IdEncargado { get; set; }
    }
}
