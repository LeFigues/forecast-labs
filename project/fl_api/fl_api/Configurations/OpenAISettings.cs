namespace fl_api.Configurations
{
    public class OpenAISettings
    {
        /// <summary>
        /// URL base de la API de OpenAI (por ejemplo: https://api.openai.com/v1/ )
        /// </summary>
        public string BaseUrl { get; set; } = string.Empty;

        /// <summary>
        /// Tu API Key de OpenAI
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// El Assistant ID que creaste en la beta de Assistants v2
        /// </summary>
        public string AssistantId { get; set; } = string.Empty;
    }
}
