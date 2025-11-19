using Microsoft.AspNetCore.Mvc;
using Project_Planner_API.Models;
using Project_Planner_API.Services;

namespace Project_Planner_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
    }
}
