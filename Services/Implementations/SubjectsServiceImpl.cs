using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Project_Planner_API.Data;
using Project_Planner_API.Data.Entities;
using Project_Planner_API.Exceptions;
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

        public async Task<IdModel> CreateSubject(SubjectCreateModel subject, Guid studentId)
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

            return new IdModel { Id = newSubject.Id };
        }

        public async Task UpdateSubject(
            Guid subjectId,
            SubjectUpdateModel subject,
            Guid studentId)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
            {
                throw new UnauthorizedAccessException();
            }

            var updatedSubject = _context.Subjects
                .Include(s => s.Result)
                .FirstOrDefault(s => s.Id == subjectId &&
                    s.Team.Any(st => st.Id == studentId));

            if (updatedSubject == null)
            {
                throw new NotFoundException(404, "Subject not found");
            }

            updatedSubject.Name = subject.Name;
            updatedSubject.Result.Name = subject.Result;
            updatedSubject.Result.Description = subject.ResultDescription;
            updatedSubject.Result.Deadline = subject.ResultDeadline;

            await _context.SaveChangesAsync();
        }
    }
}
