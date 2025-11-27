using Project_Planner_API.Models;

namespace Project_Planner_API.Services
{
    public interface ISubjectsService
    {
        public Task<List<SubjectShortModel>> GetSubjects(Guid studentId);
        public Task<SubjectModel> GetSubjectsById(Guid subjectId);
        public Task<IdModel> CreateSubject(
            SubjectCreateModel subject,
            Guid studentId);
        public Task UpdateSubject(Guid subjectId, SubjectUpdateModel subject);
        public Task DeleteSubject(Guid subjectId);
    }
}
