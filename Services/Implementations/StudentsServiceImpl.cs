using Microsoft.EntityFrameworkCore;
using Project_Planner_API.Data;
using Project_Planner_API.Data.Entities;
using Project_Planner_API.Exceptions;
using Project_Planner_API.Models;
using Project_Planner_API.Models.StudentModels;
using Project_Planner_API.Utilities;

namespace Project_Planner_API.Services.Implementations
{
    public class StudentsServiceImpl : IStudentsService
    {
        private readonly DataContext _context;
        private readonly TokenUtility _tokenUtility;

        public StudentsServiceImpl(DataContext context, TokenUtility tokenUtility)
        {
            _context = context;
            _tokenUtility = tokenUtility;
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
                PasswordHash = AuthUtility.HashPassword(student.Password)
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

            var IsPasswordRight = AuthUtility.CheckPassword(studentRegistration.PasswordHash,
                student.Password);

            if (!IsPasswordRight)
            {
                throw new UnauthorizedAccessException();
            }

            return new TokenModel { AccessToken =  _tokenUtility
                .GetToken(studentRegistration.Id) };
        }

        public async Task<ProfileModel> GetProfile(Guid studentId)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
            {
                throw new UnauthorizedAccessException();
            }

            var profile = new ProfileModel
            {
                Id = studentId,
                Name = student.Name,
                Email = student.Email
            };

            return profile;
        }
    }
}
