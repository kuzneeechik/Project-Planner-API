using Microsoft.EntityFrameworkCore;
using Project_Planner_API.Data;
using Project_Planner_API.Data.Entities;
using Project_Planner_API.Models;

namespace Project_Planner_API.Services.Implementations
{
    public class StudentsServiceImpl : IStudentsService
    {
        private readonly DataContext _context;

        public StudentsServiceImpl(DataContext context)
        {
            _context = context;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public async Task<IdModel> StudentRegistration(StudentRegistrationModel student)
        {
            if (await _context.Students.AnyAsync(s => s.Email == student.Email))
            {
                throw new Exception("Email already exists");
            }

            var studentRegistration = new StudentEntity
            {
                Name = student.Name,
                Email = student.Email,
                PasswordHash = HashPassword(student.Password)
            };

            _context.Students.Add(studentRegistration);
            await _context.SaveChangesAsync();

            return new IdModel { Id = studentRegistration.Id };
        }
    }
}
