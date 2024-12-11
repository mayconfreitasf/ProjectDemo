using Dapper;
using System.Data;
using System.Data.SqlClient;
using WebCrudAdvanced.Models.MProduct;

namespace WebCrudAdvanced.Repositories.RProduct
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProductRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }
        public async Task<int> CreateProduct(Product product)
        {
            var query = @"
                INSERT INTO Product (Name, Price, Description, UserId)
                VALUES (@Name, @Price, @Description, @UserId);
                SELECT CAST(SCOPE_IDENTITY() AS INT);"; 
            var productId = await _dbConnection.QuerySingleOrDefaultAsync<int>(query, product);
            return productId; 
        }
        public async Task<bool> UpdateProduct(Product product)
        {
            var query = @"
                UPDATE Product
                SET Name = @Name, Price = @Price, Description = @Description, UserId = @UserId
                WHERE Id = @Id;";
            var rowsAffected = await _dbConnection.ExecuteAsync(query, product);
            return rowsAffected > 0; //  if updated return true
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var query = "DELETE FROM [DBCrudAdv].[dbo].[Order] WHERE ProductId = @Id;DELETE FROM Product WHERE Id = @Id;";
            var rowsAffected = await _dbConnection.ExecuteAsync(query, new { Id = productId });
            return rowsAffected > 0; //  if deleted return true

        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var query = "SELECT * FROM Product";  // Consulta SQL simples para pegar todos os produtos
            var products = await _dbConnection.QueryAsync<Product>(query);  // Usando Dapper para executar a consulta
            return products;  // Retorna a lista de produtos
        }

        public async Task<Product> GetProductById(int productId)
        {
            var query = "SELECT * FROM Product WHERE Id = @Id";  // Consulta SQL para pegar um produto por ID
            var product = await _dbConnection.QueryFirstOrDefaultAsync<Product>(query, new { Id = productId });  // Dapper executa a consulta
            return product;  // Retorna o produto encontrado ou null caso não encontre
        }
    }
}
