using Microsoft.AspNetCore.Mvc;
using WebCrudAdvanced.Models.MProduct;
using WebCrudAdvanced.Repositories.RProduct;
using WebCrudAdvanced.Services.SProduct;

namespace WebCrudAdvanced.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this IEndpointRouteBuilder routes)
        {
            // Create Product
            routes.MapPost("/productCreate", async ([FromBody] Product product, ProductService productService) =>
            {
                if (product == null)
                {
                    return Results.BadRequest("Product data is required.");
                }

                if (string.IsNullOrEmpty(product.Name) || product.Price <= 0)
                {
                    return Results.BadRequest("Product name and valid price are required.");
                }

                try
                {
                    var productId = await productService.CreateProductWithOrdersAsync(product);
                    return Results.Created($"/api/products/{productId}", new { ProductId = productId });
                }
                catch (Exception ex)
                {
                    return Results.StatusCode(500);
                }
            });

            // Update Products
            routes.MapPut("/productEdit", async ([FromBody] Product product, [FromServices] ProductService productService) =>
            {
                var isUpdated = await productService.UpdateProduct(product);
                return isUpdated ? Results.Ok() : Results.NotFound();
            });

            // Delete Product
            routes.MapDelete("/productDelete/{id:int}", async (int id, [FromServices] ProductService productService) =>
            {
                var isDeleted = await productService.DeleteProduct(id);
                return isDeleted ? Results.Ok() : Results.NotFound();
            });

            // Get All Products
            routes.MapGet("/api/products", async (IProductRepository productRepository) =>
            {
                var products = await productRepository.GetAllProducts();
                return Results.Ok(products);
            });

            // Get Product By Id
            routes.MapGet("/productId/{id:int}", async (int id, [FromServices]  IProductRepository productRepository) =>
            {
                var product = await productRepository.GetProductById(id);
                return product != null ? Results.Ok(product) : Results.NotFound();
            });
        }
    }
}
