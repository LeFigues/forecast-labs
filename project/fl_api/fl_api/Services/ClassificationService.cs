using fl_api.Dtos;
using fl_api.Interfaces;
using fl_api.Models;
using MongoDB.Bson;

namespace fl_api.Services
{
    public class ClassificationService : IClassificationService
    {
        private readonly IStudentsApiClient _students;
        private readonly IMongoDbService _mongo;

        public ClassificationService(
            IStudentsApiClient students,
            IMongoDbService mongo)
        {
            _students = students;
            _mongo = mongo;
        }

        public async Task<ClassificationResultDto> ClassifyAsync(Guid documentId, string subjectName)
        {
            var careers = await _students.GetCareersAsync();
            var career = careers.FirstOrDefault(c =>
                c.Subjects.Any(s =>
                    string.Equals(s.Name, subjectName, StringComparison.OrdinalIgnoreCase)));
            if (career == null)
                throw new InvalidOperationException($"Subject '{subjectName}' not found.");

            var subject = career.Subjects
                .First(s => string.Equals(s.Name, subjectName, StringComparison.OrdinalIgnoreCase));

            // Creamos el registro fuertemente tipado
            var record = new ClassificationRecord
            {
                DocumentId = documentId,
                CareerId = career.Id,
                CareerName = career.Name,
                SubjectId = subject.Id,
                SubjectName = subject.Name,
                ClassifiedAt = DateTime.UtcNow
            };

            await _mongo.GetCollection<ClassificationRecord>("classifications")
                        .InsertOneAsync(record);

            // Devolvemos el DTO
            return new ClassificationResultDto
            {
                DocumentId = record.DocumentId,
                CareerId = record.CareerId,
                CareerName = record.CareerName,
                SubjectId = record.SubjectId,
                SubjectName = record.SubjectName,
                ClassifiedAt = record.ClassifiedAt
            };
        }

    }
}
