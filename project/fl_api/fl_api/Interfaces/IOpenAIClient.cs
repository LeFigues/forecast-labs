using fl_api.Dtos;

namespace fl_api.Interfaces
{
    public interface IOpenAIClient
    {
        Task<ChatCompletionResponse> CreateChatCompletionAsync(ChatCompletionRequest request);
    }
}
