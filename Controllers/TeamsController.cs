using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Planner_API.Services;

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
        [HttpGet("team/{id}")]
        public async Task<IActionResult> GetTeam([FromRoute] Guid id)
        {
            return Ok(await _teamsService.GetTeam(id));
        }

        [Authorize]
        [HttpDelete("student/{id}")]
        public async Task<IActionResult> DeleteStudent(
            [FromRoute] Guid id,
            [FromHeader] Guid subjectId)
        {
            await _teamsService.DeleteStudent(id, subjectId);

            return Ok();
        }
    }
}
