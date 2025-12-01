using Microsoft.EntityFrameworkCore;
using Project_Planner_API.Data;
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
    }
}
