namespace Order.API.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Models.Order>> GetOrderItems();
        Task<Models.Order> GetById(Guid id);
        Task<Models.Order> Add(Models.Order item);
        Task<Models.Order> Update(Models.Order item);
        void Delete(Guid id);
    }
}
