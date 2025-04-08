using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fl_forecast.Models
{
    public class InsumoPrediction
    {
        [ColumnName("Score")]
        public float PredictedCantidad { get; set; }
    }
}
