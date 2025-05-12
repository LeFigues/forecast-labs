using OpenAI.Chat;

namespace fl_api.Dtos
{
    public class ChatCompletionRequest
    {
        public string Model { get; set; } = null!;
        public List<ChatMessage> Messages { get; set; } = new();
    }
}
