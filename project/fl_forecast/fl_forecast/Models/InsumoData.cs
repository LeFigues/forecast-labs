using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fl_forecast.Models
{
    public class InsumoData
    {
        // Número de grupos en la práctica
        public float Grupos { get; set; }

        // Cantidad de insumo usada (variable a predecir)
        public float Cantidad { get; set; }
    }
}
