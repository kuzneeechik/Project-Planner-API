using Microsoft.EntityFrameworkCore;
using Project_Planner_API.Data;
using Project_Planner_API.Data.Entities;
using Project_Planner_API.Models;

namespace Project_Planner_API.Services.Implementations
{
    public class SubjectsServiceImpl : ISubjectsService
    {
        private readonly DataContext _context;

        public SubjectsServiceImpl(DataContext context)
        {
            _context = context;
        }

        public async Task<List<SubjectShortModel>> GetSubjects(Guid studentId)
        {
            var student = await _context.Students
                .Include(s => s.Subjects)
                .ThenInclude(s => s.Result)
                .FirstOrDefaultAsync(s => s.Id == studentId);
            
            if (student == null)
            {
                throw new UnauthorizedAccessException();
            }

            var subjects = student.Subjects
                .Select(s => new SubjectShortModel
                { 
                    Name = s.Name,
                    Result = s.Result.Name,
                    Deadline = s.Result.Deadline,
                    CreatedAt = s.CrearedAt
                })
                .OrderByDescending(s => s.CreatedAt)
                .ToList();

            return subjects;
        }

        public async Task CreateSubject(SubjectCreateModel subject, Guid studentId)
        {
            var newResult = new ResultEntity
            {
                Name = subject.Result,
                Description = subject.ResultDescription,
                Deadline = subject.ResultDeadline
            };

            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
            {
                throw new UnauthorizedAccessException();
            }

            var newSubject = new SubjectEntity
            {
                Name = subject.Name,
                Result = newResult,
                Team = new List<StudentEntity> { student }
            };

            student.Subjects.Add(newSubject);

            _context.Results.Add(newResult);
            _context.Subjects.Add(newSubject);

            await _context.SaveChangesAsync();
        }
    }
}
