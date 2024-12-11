using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using WebCrudAdvanced.Models.MUser;
using WebCrudAdvanced.Repositories.RUser;

namespace WebCrudAdvanced.Services.SUser
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;
        public UserService(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _jwtService = jwtService;
        }

        public async Task<AuthenticationResult> CreateUser(User user)
        {
            // Check if the user already exists
            var existingUser = await _userRepository.GetUserByUsername(user.Username);

            if (existingUser != null)
                return new AuthenticationResult { Success = false, Message = "User already exists." };

            // Encrypt the password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var result = await _userRepository.CreateUser(user);

            return result > 0
                ? new AuthenticationResult { Success = true }
                : new AuthenticationResult { Success = false, Message = "Error registering user." };
        }

        public async Task<IResult> Authenticate(User user)
        {
            var userDb = await _userRepository.GetUserByUsername(user.Username);

            if (userDb == null || !BCrypt.Net.BCrypt.Verify(user.Password, userDb.PasswordHash))
                return null;

            //token JWT
            var tokenJwt = _jwtService.GenerateJwtToken(userDb);
            var response = new
            {
                token = tokenJwt,
                user = userDb
            };
            var jsonResponse = Results.Ok(response);
            return jsonResponse;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _userRepository.GetUserByUsername(username);
        }
    }
}
