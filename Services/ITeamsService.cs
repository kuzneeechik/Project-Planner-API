using Project_Planner_API.Models;

namespace Project_Planner_API.Services
{
    public interface ITeamsService
    {
        public Task<List<StudentModel>> GetTeam(Guid subjectId);
        public Task DeleteStudent(Guid studentId, Guid subjectId);
    }
}
