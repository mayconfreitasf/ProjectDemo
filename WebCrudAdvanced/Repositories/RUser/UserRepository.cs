using System.Data;
using WebCrudAdvanced.Models.MUser;
using Dapper;
using System.Data.SqlClient;

namespace WebCrudAdvanced.Repositories.RUser
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var query = "SELECT * FROM [DBCrudAdv].[dbo].[User] WHERE Username = @Username";
            return await _dbConnection.QueryFirstOrDefaultAsync<User>(query, new { Username = username });
        }


        public async Task<int> CreateUser(User user)
        {
            // Defina sua consulta SQL para inserir um novo usuário
            var query = @"
                INSERT INTO [DBCrudAdv].[dbo].[User] (Username, PasswordHash) 
                VALUES (@Username, @PasswordHash);
                SELECT CAST(SCOPE_IDENTITY() AS INT);"; // Retorna o ID do usuário inserido

            // Execute a consulta com os parâmetros do usuário e retorne o ID do usuário inserido
            var userId = await _dbConnection.QuerySingleOrDefaultAsync<int>(query, new
            {
                user.Username,
                user.PasswordHash
            });

            return userId; // Retorna o ID do usuário inserido
        }
    }
}
