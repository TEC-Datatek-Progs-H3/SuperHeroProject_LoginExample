namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<TeamResponse> teamResponses = await _teamService.GetAll();

                if (teamResponses == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (teamResponses.Count == 0)
                {
                    return NoContent();
                }

                return Ok(teamResponses);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{teamId}")]
        public async Task<IActionResult> GetById(int teamId)
        {
            try
            {
                TeamResponse teamResponse = await _teamService.GetById(teamId);

                if (teamResponse == null)
                {
                    return NotFound();
                }

                return Ok(teamResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeamRequest teamRequest)
        {
            try
            {
                TeamResponse teamResponse = await _teamService.Create(teamRequest);

                if (teamResponse == null)
                {
                    return Problem("Team was not created, something failed...");
                }

                return Ok(teamResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{teamId}")]
        public async Task<IActionResult> Update([FromRoute] int teamId, TeamRequest teamRequest)
        {
            try
            {
                TeamResponse teamResponse = await _teamService.Update(teamId, teamRequest);

                if (teamResponse == null)
                {
                    return NotFound();
                }

                return Ok(teamResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{teamId}")]
        public async Task<IActionResult> Delete([FromRoute] int teamId)
        {
            try
            {
                TeamResponse teamResponse = await _teamService.Delete(teamId);

                if (teamResponse == null)
                {
                    return NotFound();
                }

                return Ok(teamResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
