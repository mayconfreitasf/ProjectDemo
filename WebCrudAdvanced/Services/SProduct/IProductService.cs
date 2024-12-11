using WebCrudAdvanced.Models.MProduct;

namespace WebCrudAdvanced.Services.SProduct
{
    public interface IProductService
    {
        Task<int> CreateProductWithOrdersAsync(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int productId);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int productId);
    }

}
