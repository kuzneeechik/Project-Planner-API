using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Planner_API.Services;
using Project_Planner_API.Utilities;

namespace Project_Planner_API.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectsService _subjectService;

        public SubjectsController(ISubjectsService subjectService)
        { 
            _subjectService = subjectService;
        }

        [Authorize]
        [HttpGet("subjects")]
        public async Task<IActionResult> GetSubjects()
        {
            var studentId = AuthUtility.GetUserId(User);

            return Ok(await _subjectService.GetSubjects(studentId));
        }
    }
}
