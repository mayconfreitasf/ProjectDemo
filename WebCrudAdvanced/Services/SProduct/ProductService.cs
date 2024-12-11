using WebCrudAdvanced.Models.MProduct;
using WebCrudAdvanced.Repositories.RProduct;
using WebCrudAdvanced.Services.SOrder;

namespace WebCrudAdvanced.Services.SProduct
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderService _orderService;

        public ProductService(IProductRepository productRepository, IOrderService orderService)
        {
            _productRepository = productRepository;
            _orderService = orderService;
        }

        public async Task<int> CreateProductWithOrdersAsync(Product product)
        {
           
            var productId = await _productRepository.CreateProduct(product);
            //Create orders for the product - only to demonstrate the use of tasks.
            await _orderService.CreateOrdersForProductAsync(productId, product.UserId);

            return productId; 
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            return await _productRepository.UpdateProduct(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            return await _productRepository.DeleteProduct(productId);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _productRepository.GetProductById(productId);
        }
    }
}
