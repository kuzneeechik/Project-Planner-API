using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Planner_API.Models.SubjectModels;
using Project_Planner_API.Services;
using Project_Planner_API.Utilities;

namespace Project_Planner_API.Controllers
{
    [ApiController]
    [Route("team")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamsService _teamsService;

        public TeamsController(ITeamsService teamService)
        {
            _teamsService = teamService;
        }

        [Authorize]
        [HttpGet("{subjectId}")]
        public async Task<IActionResult> GetTeam([FromRoute] Guid subjectId)
        {
            return Ok(await _teamsService.GetTeam(subjectId));
        }

        [Authorize]
        [HttpDelete("student/{studentId}")]
        public async Task<IActionResult> DeleteStudent(
            [FromRoute] Guid studentId,
            [FromHeader] Guid subjectId)
        {
            await _teamsService.DeleteStudent(studentId, subjectId);

            return Ok();
        }

        [Authorize]
        [HttpDelete("exit/{subjectId}")]
        public async Task<IActionResult> ExitStudent([FromRoute] Guid subjectId)
        {
            var studentId = AuthUtility.GetUserId(User);

            await _teamsService.DeleteStudent(studentId, subjectId);

            return Ok();
        }

        [Authorize]
        [HttpPost("entry")]
        public async Task<IActionResult> EntryStudent([FromBody] EntryModel code)
        {
            var studentId = AuthUtility.GetUserId(User);

            return Ok(await _teamsService.EntryStudent(studentId, code));
        }
    }
}
