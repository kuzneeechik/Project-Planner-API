using Microsoft.EntityFrameworkCore;
using Project_Planner_API.Data;
using Project_Planner_API.Data.Entities;
using Project_Planner_API.Exceptions;
using Project_Planner_API.Models;
using Project_Planner_API.Models.TaskModels;
using System.Security.Cryptography.Xml;

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
                .OrderBy(t => t.CreatedAt)
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
                .Include(t => t.ResponsibleStudents)
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

        public async Task<IdModel> CreateTask(TaskCreateModel task, Guid subjectId)
        {
            var subject = await _context.Subjects
                .Include(s => s.Result)
                .FirstOrDefaultAsync(s => s.Id == subjectId);

            if (subject == null)
            {
                throw new NotFoundException(404, "Subject not found");
            }

            var responsibleStudents = new List<StudentEntity>();

            for (int i = 0; i < task.ResponsibleStudents.Count; i++)
            {
                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.Id == task.ResponsibleStudents[i]);

                if (student == null)
                {
                    throw new NotFoundException(404, "Student not found");
                }

                responsibleStudents.Add(student);
            }

            var newTask = new TaskEntity
            {
                Number = task.Number,
                Name = task.Name,
                Description = task.Description,
                Deadline = task.Deadline,
                ResponsibleStudents = responsibleStudents,
                Result = subject.Result
            };

            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();

            return new IdModel { Id = newTask.Id };
        }

        public async Task<IdModel> AddSubtask(TaskCreateModel task, Guid parentId)
        {
            var parentTask = await _context.Tasks
                .Include(t => t.Result)
                .FirstOrDefaultAsync(t => t.Id == parentId);

            if (parentTask == null)
            {
                throw new NotFoundException(404, "Task not found");
            }

            var responsibleStudents = new List<StudentEntity>();

            for (int i = 0; i < task.ResponsibleStudents.Count; i++)
            {
                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.Id == task.ResponsibleStudents[i]);

                if (student == null)
                {
                    throw new NotFoundException(404, "Student not found");
                }

                responsibleStudents.Add(student);
            }

            var newTask = new TaskEntity
            {
                Number = task.Number,
                Name = task.Name,
                Description = task.Description,
                Deadline = task.Deadline,
                ResponsibleStudents = responsibleStudents,
                Result = parentTask.Result,
                ParentTask = parentTask
            };

            parentTask.SubTasks.Add(newTask);

            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();

            return new IdModel { Id = newTask.Id };
        }

        public async Task UpdateTask(Guid taskId, TaskUpdateModel task)
        {
            var updateTask = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (updateTask == null)
            {
                throw new NotFoundException(404, "Task not found");
            }

            updateTask.Name = task.Name;
            updateTask.Description = task.Description;
            updateTask.Deadline = task.Deadline;

            var responsibleStudents = new List<StudentEntity>();

            for (int i = 0; i < task.ResponsibleStudents.Count; i++)
            {
                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.Id == task.ResponsibleStudents[i]);

                if (student == null)
                {
                    throw new NotFoundException(404, "Student not found");
                }

                responsibleStudents.Add(student);
            }

            updateTask.ResponsibleStudents = responsibleStudents;

            await _context.SaveChangesAsync();
        }

        public async Task ChangeTaskStatus(Guid taskId, StatusModel status)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
            {
                throw new NotFoundException(404, "Task not found");
            }

            task.Status = status.Status;
            task.CreatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}
