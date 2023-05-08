namespace SuperHeroAPI.Controllers
{
    [Authorize(Role.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IJwtUtils _jwtUtils;

        public UserController(IUserRepository userRepository, IJwtUtils jwtUtils)
        {
            //_userService = userService;
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest login)
        {
            try
            {
                User user = await _userRepository.GetByEmail(login.Email);
                if (user == null)
                {
                    return Unauthorized();
                }

                if (user.Password == login.Password)
                {
                    return Ok(new LoginResponse
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Username = user.Username,
                        Role = user.Role,
                        Token = _jwtUtils.GenerateJwtToken(user)
                    });
                }

                return Unauthorized();
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
                User user = new()
                {
                    Email = newUser.Email,
                    Username = newUser.Username,
                    Password = newUser.Password,
                    Role = Helpers.Role.User // force all users created through Register, to Role.User
                };

                user = await _userRepository.Create(user);

                return Ok(MapUserTouserResponse(user));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

     
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<User> users = await _userRepository.GetAll();

                if (users == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (users.Count == 0)
                {
                    return NoContent();
                }

                return Ok(users.Select(user => MapUserTouserResponse(user)).ToList());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Role.Admin, Role.User)]
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

                User user = await _userRepository.GetById(userId);


                if (user == null)
                {
                    return NoContent();
                }

                return Ok(MapUserTouserResponse(user));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        public static UserResponse MapUserTouserResponse(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role
            };
        }

    }
}
