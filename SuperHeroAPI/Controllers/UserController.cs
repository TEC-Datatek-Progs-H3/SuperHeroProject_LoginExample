namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest login)
        {
            try
            {
                LoginResponse response = await _userService.AuthenticateUser(login);

                if (response == null)
                {
                    return Unauthorized();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser newUser)
        {
            try
            {
                UserResponse user = await _userService.RegisterUser(newUser);
                return Ok(user);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Role.Admin)] // only admins are allowed entry to this endpoint
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<UserResponse> users = await _userService.GetAllUsers();

                if (users == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (users.Count == 0)
                {
                    return NoContent();
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Role.User, Role.Admin)]
        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetById([FromRoute] int userId)
        {
            try
            {
                // only admins can access other user records
                UserResponse currentUser = (UserResponse)HttpContext.Items["User"];
                if (currentUser != null && userId != currentUser.Id && currentUser.Role != Role.Admin)
                {
                    return Unauthorized(new { message = "Unauthorized" });
                }

                UserResponse user = await _userService.GetById(userId);

                if (user == null)
                {
                    return NoContent();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
