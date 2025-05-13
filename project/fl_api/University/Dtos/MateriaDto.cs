using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Dtos
{
    public class MateriaDto
    {
        public string Nombre { get; set; } = null!;
        public int SemestreNumero { get; set; }
        public string CarreraNombre { get; set; } = null!;
    }
}
