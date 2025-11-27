using Project_Planner_API.Models;

namespace Project_Planner_API.Services
{
    public interface ISubjectsService
    {
        public Task<List<SubjectShortModel>> GetSubjects(Guid studentId);
        public Task CreateSubject(SubjectCreateModel subject, Guid studentId);
    }
}
