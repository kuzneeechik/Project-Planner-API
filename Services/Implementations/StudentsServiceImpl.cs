using Microsoft.EntityFrameworkCore;
using Project_Planner_API.Data;
using Project_Planner_API.Data.Entities;
using Project_Planner_API.Exceptions;
using Project_Planner_API.Models;

namespace Project_Planner_API.Services.Implementations
{
    public class StudentsServiceImpl : IStudentsService
    {
        private readonly DataContext _context;
        private readonly ITokensService _tokensService;

        public StudentsServiceImpl(DataContext context, ITokensService tokensService)
        {
            _context = context;
            _tokensService = tokensService;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool CheckPassword(string hashedPassword, string enteredPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
        }

        public async Task<IdModel> StudentRegistration(StudentRegistrationModel student)
        {
            if (await _context.Students.AnyAsync(s => s.Email == student.Email))
            {
                throw new FieldAlreadyExistException(400, "Email already exist");
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

        public async Task<TokenModel> StudentLogIn(LogInModel student)
        {
            var studentRegistration = await _context.Students
                .FirstOrDefaultAsync(s => s.Email == student.Email);

            if (studentRegistration == null)
            {
                throw new UnauthorizedAccessException();
            }

            var IsPasswordRight = CheckPassword(studentRegistration.PasswordHash,
                student.Password);

            if (!IsPasswordRight)
            {
                throw new UnauthorizedAccessException();
            }

            return new TokenModel { AccessToken =  _tokensService
                .GetToken(studentRegistration.Id) };
        }
    }
}
