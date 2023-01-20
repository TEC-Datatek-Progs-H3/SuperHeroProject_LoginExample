using SuperHeroAPI.Controllers;

namespace SuperHeroAPI.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserRepository userRepository, IJwtUtils jwtUtils)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            int? userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                var user = await userRepository.GetById(userId.Value);
                context.Items["User"] = UserController.MapUserTouserResponse(user);
            }

            await _next(context);
        }
    }
}
