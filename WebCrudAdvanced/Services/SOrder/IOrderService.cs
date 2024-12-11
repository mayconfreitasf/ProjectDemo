namespace WebCrudAdvanced.Services.SOrder
{
    public interface IOrderService
    {
        Task CreateOrdersForProductAsync(int productId, int userId);
    }
}
