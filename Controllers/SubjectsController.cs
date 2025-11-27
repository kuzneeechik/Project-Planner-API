using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Planner_API.Models;
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

        [Authorize]
        [HttpPost("subject")]
        public async Task<IActionResult> CreateSubject([FromBody] SubjectCreateModel subject)
        {
            var studentId = AuthUtility.GetUserId(User);

            return Created("", await _subjectService
                .CreateSubject(subject, studentId));
        }

        [Authorize]
        [HttpPut("subject/{id}")]
        public async Task<IActionResult> UpdateSubject(
            [FromRoute] Guid id,
            [FromBody] SubjectUpdateModel subject)
        {
            var studentId = AuthUtility.GetUserId(User);

            await _subjectService.UpdateSubject(id, subject, studentId);

            return Ok();
        }
    }
}
