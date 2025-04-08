using fl_forecast.Models;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fl_forecast.ML
{
    public static class ModelBuilder
    {
        private static readonly MLContext mlContext = new();

        public static ITransformer TrainModel(out DataViewSchema inputSchema)
        {
            // Datos de ejemplo. Luego puedes reemplazar con lectura desde CSV.
            var data = new List<InsumoData>
            {
                new InsumoData { Grupos = 1, Cantidad = 2 },
                new InsumoData { Grupos = 2, Cantidad = 4 },
                new InsumoData { Grupos = 3, Cantidad = 6 },
                new InsumoData { Grupos = 4, Cantidad = 8 },
                new InsumoData { Grupos = 5, Cantidad = 10 },
            };

            var trainingData = mlContext.Data.LoadFromEnumerable(data);

            // Pipeline de entrenamiento
            var pipeline = mlContext.Transforms.CopyColumns("Label", nameof(InsumoData.Cantidad))
                .Append(mlContext.Transforms.Concatenate("Features", nameof(InsumoData.Grupos)))
                .Append(mlContext.Regression.Trainers.Sdca());

            // Entrenamiento
            var model = pipeline.Fit(trainingData);

            // Evaluación del modelo (opcional pero útil)
            var predictions = model.Transform(trainingData);
            var metrics = mlContext.Regression.Evaluate(predictions);
            System.Console.WriteLine($"📈 R²: {metrics.RSquared:0.##} | RMSE: {metrics.RootMeanSquaredError:0.##}");

            inputSchema = trainingData.Schema;
            return model;
        }

        public static PredictionEngine<InsumoData, InsumoPrediction> CreatePredictor(ITransformer model)
        {
            return mlContext.Model.CreatePredictionEngine<InsumoData, InsumoPrediction>(model);
        }
    }
}
