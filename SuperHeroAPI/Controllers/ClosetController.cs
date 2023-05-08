using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClosetController : ControllerBase
    {
        private readonly IClosetRepository _closetRepository;

        public ClosetController(IClosetRepository closetRepository)
        {
            _closetRepository = closetRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Closet>>> GetAllClosets()
        {
            try
            {
                var closets = await _closetRepository.GetAllAsync();

                return Ok(closets.Select(x => x.ToResponse()).ToList());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("Apparels")]
        public async Task<ActionResult<List<ApparelResponse>>> GetAllApparels()
        {
            try
            {
                var apparels = await _closetRepository.GetAllApparelsAsync();
                return Ok(apparels.Select(a => a.ToResponse()).ToList());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
