using Microsoft.EntityFrameworkCore;
using Project_Planner_API.Data;
using Project_Planner_API.Exceptions;
using Project_Planner_API.Models;

namespace Project_Planner_API.Services.Implementations
{
    public class TeamsServiceImpl : ITeamsService
    {
        private readonly DataContext _context;

        public TeamsServiceImpl(DataContext context)
        {
            _context = context;
        }

        public async Task<List<StudentModel>> GetTeam(Guid subjectId)
        {
            var subject = await _context.Subjects
                .Include(s => s.Team)
                .FirstOrDefaultAsync(s => s.Id == subjectId);

            if (subject == null)
            {
                throw new NotFoundException(404, "Subject not found");
            }

            var team = subject.Team
                .Select(s => new StudentModel
                {
                    Name = s.Name
                })
                .OrderBy(s => s.Name)
                .ToList();

            return team;
        }

        public async Task DeleteStudent(Guid studentId, Guid subjectId)
        {
            var subject = await _context.Subjects
                .Include(s => s.Team)
                .FirstOrDefaultAsync(s => s.Id == subjectId &&
                    s.Team.Any(st => st.Id == studentId));

            if (subject == null)
            {
                throw new NotFoundException(404, "Subject not found");
            }

            var student = await _context.Students
                .Include(s => s.Subjects)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
            {
                throw new NotFoundException(404, "Student not found");
            }

            subject.Team.Remove(student);
            student.Subjects.Remove(subject);

            await _context.SaveChangesAsync();
        }
    }
}
