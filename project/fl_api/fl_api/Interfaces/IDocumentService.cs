using fl_api.Dtos;
using fl_api.Models;

namespace fl_api.Interfaces
{
    public interface IDocumentService
    {
        Task<DocumentRecord> SaveDocumentAsync(UploadDocumentDto dto);
    }
}
