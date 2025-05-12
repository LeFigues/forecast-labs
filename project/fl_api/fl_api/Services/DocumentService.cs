using fl_api.Configurations;
using fl_api.Dtos;
using fl_api.Interfaces;
using fl_api.Models;
using Microsoft.Extensions.Options;

namespace fl_api.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly PythonConfigRoutes _routes;
        private readonly IMongoDbService _mongo;
        private readonly IStudentsApiClient _studentsApi;
        private readonly string _storageFolder;

        public DocumentService(
            IOptions<PythonConfigRoutes> options,
            IMongoDbService mongo,
            IStudentsApiClient studentsApi)
        {
            _routes = options.Value;
            _mongo = mongo;
            _studentsApi = studentsApi;
            // Use PermanentFolder as storage
            _storageFolder = Path.IsPathRooted(_routes.PermanentFolder)
                ? _routes.PermanentFolder
                : Path.Combine(_routes.BasePath, _routes.PermanentFolder);
        }

        public async Task<DocumentRecord> SaveDocumentAsync(UploadDocumentDto dto)
        {
            // Rename file
            var id = Guid.NewGuid();
            var ext = Path.GetExtension(dto.File.FileName);
            var storedName = $"{id}{ext}";

            Directory.CreateDirectory(_storageFolder);
            var fullPath = Path.Combine(_storageFolder, storedName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            // Fetch career and subject info
            var careerResp = await _studentsApi.GetCareersAsync();
            var career = careerResp.First(c => c.Id == dto.CareerId);
            var subject = career.Subjects.First(s => s.Id == dto.SubjectId);

            // Build document record
            var record = new DocumentRecord
            {
                Id = id,
                OriginalFileName = dto.File.FileName,
                StoredFileName = storedName,
                UploadedAt = DateTime.UtcNow,
                CareerId = career.Id,
                CareerName = career.Name,
                SubjectId = subject.Id,
                SubjectName = subject.Name
            };

            // Save to Mongo
            var col = _mongo.GetCollection<DocumentRecord>("Documents");
            await col.InsertOneAsync(record);
            return record;
        }
    }
}
