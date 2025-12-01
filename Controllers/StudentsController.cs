using Microsoft.AspNetCore.Mvc;
using Project_Planner_API.Models.StudentModels;
using Project_Planner_API.Services;
using Project_Planner_API.Utilities;

namespace Project_Planner_API.Controllers
{
    [ApiController]
    [Route("student")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsService _studentsService;

        public StudentsController(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> StudentRegistration(
            [FromBody] StudentRegistrationModel student)
        {
            return Ok(await _studentsService.StudentRegistration(student));
        }

        [HttpPost("login")]
        public async Task<IActionResult> StudentLogIn([FromBody] LogInModel student)
        {
            return Ok(await _studentsService.StudentLogIn(student));
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var studentId = AuthUtility.GetUserId(User);

            return Ok(await _studentsService.GetProfile(studentId));
        }
    }
}
