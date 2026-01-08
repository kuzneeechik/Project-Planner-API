using Microsoft.EntityFrameworkCore;
using Project_Planner_API.Data;
using Project_Planner_API.Exceptions;
using Project_Planner_API.Models;
using Project_Planner_API.Models.StudentModels;
using Project_Planner_API.Models.SubjectModels;

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
                    Id = s.Id,
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
                .Include(s => s.Tasks)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
            {
                throw new NotFoundException(404, "Student not found");
            }

            for (int i = 0; i < student.Tasks.Count; i++)
            {
                student.Tasks[i].ResponsibleStudents.Remove(student);
            }

            subject.Team.Remove(student);
            student.Subjects.Remove(subject);

            await _context.SaveChangesAsync();
        }

        public async Task<IdModel> EntryStudent(Guid studentId, EntryModel code)
        {
            var student = await _context.Students
                .Include(s => s.Subjects)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
            {
                throw new UnauthorizedAccessException();
            }

            var subject = await _context.Subjects
                .Include(s => s.Team)
                .FirstOrDefaultAsync(s => s.Code == code.Code);

            if (subject == null)
            {
                throw new NotFoundException(400, "Wrong subject code");
            }

            subject.Team.Add(student);
            student.Subjects.Add(subject);

            await _context.SaveChangesAsync();

            return new IdModel { Id = subject.Id };
        }
    }
}
