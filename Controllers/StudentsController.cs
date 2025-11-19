using Microsoft.AspNetCore.Mvc;
using Project_Planner_API.Models;
using Project_Planner_API.Services;

namespace Project_Planner_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsService _studentService;

        public StudentsController(IStudentsService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("register")]
        public async Task<IActionResult> StudentRegistration(
            [FromBody] StudentRegistrationModel student)
        {
            try
            {
                return Ok(_studentService.StudentRegistration(student));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
