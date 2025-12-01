using Project_Planner_API.Models.TaskModels;

namespace Project_Planner_API.Services
{
    public interface ITasksService
    {
        public Task<List<TaskShortModel>> GetTasks(Guid subjectId);
        public Task<TaskModel> GetTaskById(Guid taskId);
    }
}
