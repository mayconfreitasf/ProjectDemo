using System.Data;
using WebCrudAdvanced.Models.MOrder;

namespace WebCrudAdvanced.Repositories.ROrder
{
    public interface IOrderRepository
    {
        Task CreateOrderAsync(Order order);

    }
}
