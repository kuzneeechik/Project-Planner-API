using Project_Planner_API.Models.StudentModels;
using Project_Planner_API.Models.SubjectModels;

namespace Project_Planner_API.Services
{
    public interface ITeamsService
    {
        public Task<List<StudentModel>> GetTeam(Guid subjectId);
        public Task DeleteStudent(Guid studentId, Guid subjectId);
        public Task EntryStudent(Guid studentId, Guid subjectId, EntryModel code);
    }
}
