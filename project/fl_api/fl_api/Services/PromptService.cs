using System;
using fl_api.Configurations;
using fl_api.Interfaces;
using Microsoft.Extensions.Options;

namespace fl_api.Services
{
    public class PromptService : IPromptService
    {
        private readonly PromptSettings _settings;
        public PromptService(IOptions<PromptSettings> opts)
            => _settings = opts.Value;

        // Implementamos exactamente el método de la interfaz
        public string GetPrompt(string key) => key switch
        {
            "correction" => _settings.Correction,
            "structuring" => _settings.Structuring,
            "prediction" => _settings.Prediction,
            _ => throw new KeyNotFoundException($"Prompt '{key}' not found")
        };
    }
}
