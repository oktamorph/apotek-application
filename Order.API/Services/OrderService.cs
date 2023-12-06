using Microsoft.EntityFrameworkCore;
using Order.API.DBContext;

namespace Order.API.Services
{
    public class OrderService : IOrderService
    {
        #region Variables
        private readonly OrderContext _orderContext;
        #endregion
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="orderContext">OrderContext instance.</param>
        public OrderService(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }
        /// <summary>
        /// The method that returns all order items from the database.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Models.Order>> GetOrderItems()
        {
            return await _orderContext.Orders.ToListAsync();
        }
        /// <summary>
        /// The method that returns the order by guidId from the database.
        /// </summary>
        /// <param name="guidId">GuidID parameter.</param>
        /// <returns></returns>
        public async Task<Models.Order> GetById(Guid guidId)
        {
            return await _orderContext.Orders.FirstOrDefaultAsync(x => x.OrderGuid == guidId);
        }
        /// <summary>
        /// The method that adds new order in the database.
        /// </summary>
        /// <param name="order">Order instance.</param>
        /// <returns></returns>
        public async Task<Models.Order> Add(Models.Order order)
        {
            var result = await _orderContext.Orders.AddAsync(order);
            await _orderContext.SaveChangesAsync();

            return result.Entity;
        }
        /// <summary>
        /// The method that updates the order in the database.
        /// </summary>
        /// <param name="order">Order instance.</param>
        /// <returns></returns>
        public async Task<Models.Order> Update(Models.Order order)
        {
            var result = await _orderContext.Orders.FirstOrDefaultAsync(x => x.OrderGuid == order.OrderGuid);

            if (result != null)
            {
                result.Description = order.Description;
                result.Price = order.Price;

                await _orderContext.SaveChangesAsync();
            }

            return result;
        }
        /// <summary>
        /// The method that deletes the order from the database.
        /// </summary>
        /// <param name="guidId">GuidID parameter.</param>
        public async void Delete(Guid guidId)
        {
            _orderContext.Orders.Where(x => x.OrderGuid == guidId).ExecuteDelete();
            await _orderContext.SaveChangesAsync();
        }
    }
}

