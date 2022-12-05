namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroService _superHeroService;

        public SuperHeroController(ISuperHeroService superHeroService)
        {
            _superHeroService = superHeroService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<SuperHeroResponse> superHeroes = await _superHeroService.GetAll();

                if (superHeroes == null)
                {
                    return Problem("Nothing was returned from service, this is unexpected");
                }

                if (superHeroes.Count() == 0)
                {
                    return NoContent();
                }

                return Ok(superHeroes);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{superHeroId}")]
        public async Task<IActionResult> GetById([FromRoute] int superHeroId)
        {
            try
            {
                SuperHeroResponse superHeroResponse = await _superHeroService.GetById(superHeroId);

                if (superHeroResponse == null)
                {
                    return NotFound();
                }

                return Ok(superHeroResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(SuperHeroRequest superHeroRequest)
        {
            try
            {
                SuperHeroResponse superHeroResponse = await _superHeroService.Create(superHeroRequest);

                if (superHeroResponse == null)
                {
                    return Problem("SuperHero was not created, something failed...");
                }
                    
                return Ok(superHeroResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{superHeroId}")]
        public async Task<IActionResult> Update([FromRoute] int superHeroId, SuperHeroRequest superHeroRequest)
        {
            try
            {
                SuperHeroResponse superHeroResponse = await _superHeroService.Update(superHeroId, superHeroRequest);

                if (superHeroResponse == null)
                {
                    return NotFound();
                }

                return Ok(superHeroResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{superHeroId}")]
        public async Task<IActionResult> Delete([FromRoute] int superHeroId)
        {
            try
            {
                SuperHeroResponse superHeroResponse = await _superHeroService.Delete(superHeroId);

                if (superHeroResponse == null)
                {
                    return NotFound();
                }

                return Ok(superHeroResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
