using WebCrudAdvanced.Models.MUser;

namespace WebCrudAdvanced.Services.SUser
{
    public interface IUserService
    {
        Task<AuthenticationResult> CreateUser(User user);
        Task<IResult> Authenticate(User user);
        Task<User> GetUserByUsername(string username);
    }
}
