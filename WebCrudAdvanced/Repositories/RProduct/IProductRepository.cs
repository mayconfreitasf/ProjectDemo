using WebCrudAdvanced.Models.MProduct;

namespace WebCrudAdvanced.Repositories.RProduct
{
    public interface IProductRepository
    {
        Task<int> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int productId);

        Task<IEnumerable<Product>> GetAllProducts();

        Task<Product> GetProductById(int productId); 
    }
}
