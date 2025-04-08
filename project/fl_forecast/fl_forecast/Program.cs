using System;
using fl_forecast.Models;
using fl_forecast.ML;
using Microsoft.ML;

class Program
{
    static void Main()
    {
        Console.WriteLine("🔮 Entrenando modelo de predicción de insumos...");

        // Entrenar el modelo y obtener el schema
        var model = ModelBuilder.TrainModel(out DataViewSchema inputSchema);

        // Crear motor de predicción
        var predictor = ModelBuilder.CreatePredictor(model);

        // Simular entrada del usuario (por ejemplo, 10 grupos)
        Console.WriteLine("Ingrese el número de grupos:");
        var entradaTexto = Console.ReadLine();
        if (!float.TryParse(entradaTexto, out float grupos))
        {
            Console.WriteLine("❌ Entrada no válida. Debe ser un número.");
            return;
        }

        var insumo = new InsumoData { Grupos = grupos };
        var resultado = predictor.Predict(insumo);

        // Mostrar el resultado en JSON
        Console.WriteLine("\n📦 Predicción JSON:");
        var json = System.Text.Json.JsonSerializer.Serialize(new
        {
            grupos = insumo.Grupos,
            cantidad_predicha = resultado.PredictedCantidad
        }, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });

        Console.WriteLine(json);
    }
}
