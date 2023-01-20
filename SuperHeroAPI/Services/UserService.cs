namespace SuperHeroAPI.Services
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllUsers();
        Task<UserResponse> GetById(int userId);
        Task<LoginResponse> AuthenticateUser(LoginRequest login);
        Task<UserResponse> RegisterUser(RegisterUser newUser);
        Task<UserResponse> Update(int userId, UpdateUser updateUser);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtUtils _jwtUtils;

        public UserService(IUserRepository userRepository, IJwtUtils jwtUtils)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
        }

        public async Task<LoginResponse> AuthenticateUser(LoginRequest login)
        {
            User user = await _userRepository.GetByEmail(login.Email);
            if (user == null)
            {
                return null;
            }

            if (user.Password == login.Password)
            {
                LoginResponse response = new()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Username = user.Username,
                    Role = user.Role,
                    Token = _jwtUtils.GenerateJwtToken(user)
                };
                return response;
            }

            return null;
        }

        public async Task<List<UserResponse>> GetAllUsers()
        {
            List<User> users = await _userRepository.GetAll();

            return users?.Select(user => MapUserTouserResponse(user)).ToList();
        }

        public async Task<UserResponse> GetById(int userId)
        {
            User user = await _userRepository.GetById(userId);
            return MapUserTouserResponse(user);
        }

        public async Task<UserResponse> RegisterUser(RegisterUser newUser)
        {
            User user = new()
            {
                Email = newUser.Email,
                Username = newUser.Username,
                Password = newUser.Password,
                Role = Helpers.Role.User // force all users created through Register, to Role.User
            };

            user = await _userRepository.Create(user);

            return MapUserTouserResponse(user);
        }

        public async Task<UserResponse> Update(int userId, UpdateUser updateUser)
        {
            User user = new()
            {
                Email = updateUser.Email,
                Username = updateUser.Username,
                Password = updateUser.Password,
                Role = updateUser.Role
            };

            user = await _userRepository.Update(userId, user);

            return MapUserTouserResponse(user);
        }

        static private UserResponse MapUserTouserResponse(User user)
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
