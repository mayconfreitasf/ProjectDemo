using WebCrudAdvanced.Models.MUser;

namespace WebCrudAdvanced.Repositories.RUser
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsername(string username);
        Task<int> CreateUser(User user);
    }
}
