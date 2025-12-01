using Project_Planner_API.Models;
using Project_Planner_API.Models.TaskModels;

namespace Project_Planner_API.Services
{
    public interface ITasksService
    {
        public Task<List<TaskShortModel>> GetTasks(Guid subjectId);
        public Task<TaskModel> GetTaskById(Guid taskId);
        public Task<IdModel> CreateTask(TaskCreateModel task, Guid subjectId);
        public Task<IdModel> AddSubtask(TaskCreateModel task, Guid parentId);
        public Task UpdateTask(Guid taskId, TaskUpdateModel task);
    }
}
