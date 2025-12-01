using Microsoft.EntityFrameworkCore;
using Project_Planner_API.Data;
using Project_Planner_API.Exceptions;
using Project_Planner_API.Models.TaskModels;

namespace Project_Planner_API.Services.Implementations
{
    public class TasksServiceImpl : ITasksService
    {
        private readonly DataContext _context;

        public TasksServiceImpl(DataContext context)
        {
            _context = context;
        }

        public async Task<List<TaskShortModel>> GetTasks(Guid subjectId)
        {
            var tasks = await _context.Tasks
                .Include(t => t.Result)
                .ThenInclude(r => r.Subject)
                .Where(t => t.Result.Subject!.Id == subjectId)
                .Select(t => new TaskShortModel
                {
                    Id = t.Id,
                    Number = t.Number,
                    Name = t.Name,
                    Status = t.Status
                })
                .ToListAsync();

            return tasks;
        }

        public async Task<TaskModel> GetTaskById(Guid taskId)
        {
            var task = await _context.Tasks
                .Include(t => t.Result)
                .Include(t => t.ParentTask)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
            {
                throw new NotFoundException(404, "Task not found");
            }

            var currentTask = new TaskModel
            {
                Id = taskId,
                Number = task.Number,
                Name = task.Name,
                Status = task.Status,
                Deadline = task.Deadline,
                ParentNumber = task.ParentTask == null ? null
                    : task.ParentTask.Number,
                ParentName = task.ParentTask == null ? task.Result.Name
                    : task.ParentTask.Name,
                Description = task.Description,
                ResponsibleStudents = task.ResponsibleStudents
                    .Select(s => s.Name)
                    .ToList(),
            };

            return currentTask;
        }
    }
}
