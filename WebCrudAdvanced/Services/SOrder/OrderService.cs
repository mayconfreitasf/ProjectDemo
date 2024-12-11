using WebCrudAdvanced.Models.MOrder;
using WebCrudAdvanced.Repositories.ROrder;

namespace WebCrudAdvanced.Services.SOrder
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private static SemaphoreSlim _semaphore = new SemaphoreSlim(10);
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task CreateOrdersForProductAsync(int productId, int userId)
        {
            // Create 100 orders for the product - just to demonstrate manipulation with asynchronous tasks.
            // Create semaphore to limite 10 simultaneus tasks - just to demonstrate manipulation with semaphore

            var orderTasks = new List<Task>();
            var random = new Random();
            for (int i = 0; i < 50; i++)
            {
                //wait permission semaphore
                await _semaphore.WaitAsync(); 
                var order = new Order
                {
                    ProductId = productId,
                    UserId = userId,
                    Quantity = random.Next(1, 100),
                    RegisterDate = DateTime.UtcNow,

                };
                orderTasks.Add(CreateOrderAsync(order).ContinueWith(t =>
                {
                    //release the permission after the task is completed
                    _semaphore.Release(); 
                }));
                //orderTasks.Add(CreateOrderAsync(order));
            }
            await Task.WhenAll(orderTasks);
            // Clear references to the GC collect memory
            orderTasks.Clear();

            //Force garbage collection after clearing the list
            GC.Collect();
            GC.WaitForPendingFinalizers(); 
        }

        private async Task CreateOrderAsync(Order order)
        {
            await _orderRepository.CreateOrderAsync(order);
        }
    }
}
