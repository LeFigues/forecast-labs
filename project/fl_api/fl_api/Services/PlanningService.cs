using fl_api.Dtos;
using fl_api.Interfaces;
using fl_api.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace fl_api.Services
{
    public class PlanningService : IPlanningService
    {
        private readonly IMongoCollection<PlanningRecord> _planningCol;
        private readonly IMongoCollection<BsonDocument> _openaiCol;

        public PlanningService(IMongoDbService mongo)
        {
            _planningCol = mongo.GetCollection<PlanningRecord>("planning");
            _openaiCol = mongo.GetCollection<BsonDocument>("openai");
        }

        public async Task<PlanningRecord> CreatePlanningAsync(Guid documentId, PlanningCreateDto dto)
        {
            // Recuperar análisis existente
            var aiDoc = await _openaiCol
                .Find(Builders<BsonDocument>.Filter.Eq("DocumentId", documentId.ToString()))
                .FirstOrDefaultAsync();
            if (aiDoc == null)
                throw new InvalidOperationException($"No se encontró análisis para documentId {documentId}");

            var rec = new PlanningRecord
            {
                DocumentId = documentId,
                Date = dto.Date,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Group = dto.Group,
                Classroom = dto.Classroom,
                Teacher = dto.Teacher,
                LabCode = dto.LabCode,
                Title = dto.Title,
                Groups = dto.Groups,
                Students = dto.Students
                                      .Select(s => new Student
                                      {
                                          FirstName = s.FirstName,
                                          LastName = s.LastName
                                      })
                                      .ToList(),
                AnalysisResult = aiDoc["AnalysisResult"].AsBsonDocument,  // sigue aquí para guardar
                CreatedAt = DateTime.UtcNow
            };

            await _planningCol.InsertOneAsync(rec);
            return rec;
        }

        public async Task<List<PlanningRecord>> GetAllPlanningsAsync()
            => await _planningCol.Find(_ => true).ToListAsync();

        public async Task<PlanningRecord?> GetPlanningAsync(string planningId)
        {
            if (!ObjectId.TryParse(planningId, out var oid))
                return null;
            return await _planningCol.Find(p => p.Id == oid).FirstOrDefaultAsync();
        }

        public async Task<PlanningRecord?> UpdatePlanningAsync(string planningId, PlanningUpdateDto dto)
        {
            if (!ObjectId.TryParse(planningId, out var oid))
                return null;

            var update = Builders<PlanningRecord>.Update
                .Set(p => p.Date, dto.Date)
                .Set(p => p.StartTime, dto.StartTime)
                .Set(p => p.EndTime, dto.EndTime)
                .Set(p => p.Group, dto.Group)
                .Set(p => p.Classroom, dto.Classroom)
                .Set(p => p.Teacher, dto.Teacher)
                .Set(p => p.LabCode, dto.LabCode)
                .Set(p => p.Title, dto.Title)
                .Set(p => p.Groups, dto.Groups)
                .Set(p => p.Students, dto.Students
                                                .Select(s => new Student
                                                {
                                                    FirstName = s.FirstName,
                                                    LastName = s.LastName
                                                })
                                                .ToList())
                .Set(p => p.CreatedAt, DateTime.UtcNow);

            var res = await _planningCol.UpdateOneAsync(
                p => p.Id == oid, update);

            if (res.MatchedCount == 0) return null;
            return await GetPlanningAsync(planningId);
        }

        public async Task<bool> DeletePlanningAsync(string planningId)
        {
            if (!ObjectId.TryParse(planningId, out var oid))
                return false;

            var res = await _planningCol.DeleteOneAsync(p => p.Id == oid);
            return res.DeletedCount > 0;
        }
    }
}
