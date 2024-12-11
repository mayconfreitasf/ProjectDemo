using Dapper;
using System.Data;
using System.Data.SqlClient;
using WebCrudAdvanced.Models.MOrder;
using WebCrudAdvanced.Models.MProduct;

namespace WebCrudAdvanced.Repositories.ROrder
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;


        public OrderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreateOrderAsync(Order order)
        {
            try
            {
                using (var dbConnection = new SqlConnection(_connectionString))  // Usando uma nova conexão para cada operação
                {
                    await dbConnection.OpenAsync();  // Abrir a conexão
                    var query = @"
                    INSERT INTO [DBCrudAdv].[dbo].[Order] ([ProductId], [Quantity], [RegisterDate], [UserId])
                    VALUES (@ProductId, @Quantity, @RegisterDate, @UserId)
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    var identity = await dbConnection.QuerySingleOrDefaultAsync<int>(query, order);
                }
            }
            catch (Exception ex)
            {
                // Log any exceptions for debugging
                Console.WriteLine($"Error inserting order: {ex.Message}");
            }
        }
    }
}
