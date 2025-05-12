using fl_api.Dtos;

namespace fl_api.Interfaces
{
    public interface IClassificationService
    {
        Task<ClassificationResultDto> ClassifyAsync(Guid documentId, string subjectName);
    }
}
