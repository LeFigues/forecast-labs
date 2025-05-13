using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Dtos
{
    public class InsumoPorPracticaDto
    {
        public string PracticaTitulo { get; set; } = null!;
        public string NombreInsumo { get; set; } = null!;
        public int Cantidad { get; set; }
    }
}
