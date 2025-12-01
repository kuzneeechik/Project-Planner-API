using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Planner_API.Models.SubjectModels;
using Project_Planner_API.Services;
using Project_Planner_API.Utilities;

namespace Project_Planner_API.Controllers
{   
    [ApiController]
    [Route("subject")]
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
        [HttpGet("{subjectId}")]
        public async Task<IActionResult> GetSubjectById([FromRoute] Guid subjectId)
        {
            return Ok(await _subjectService.GetSubjectsById(subjectId));
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateSubject([FromBody] SubjectCreateModel subject)
        {
            var studentId = AuthUtility.GetUserId(User);

            return Created("", await _subjectService
                .CreateSubject(subject, studentId));
        }

        [Authorize]
        [HttpPut("update/{subjectId}")]
        public async Task<IActionResult> UpdateSubject(
            [FromRoute] Guid subjectId,
            [FromBody] SubjectUpdateModel subject)
        {
            await _subjectService.UpdateSubject(subjectId, subject);

            return Ok();
        }

        [Authorize]
        [HttpDelete("delete/{subjectId}")]
        public async Task<IActionResult> DeleteSubject([FromRoute] Guid subjectId)
        {
            await _subjectService.DeleteSubject(subjectId);

            return Ok();
        }
    }
}
