using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Dtos
{
    public class PracticaDto
    {
        public string Titulo { get; set; } = null!;
        public int NumeroPractica { get; set; }
        public string Descripcion { get; set; } = null!;
        public int IdLaboratorio { get; set; }
        public int IdMateria { get; set; }
    }
}
