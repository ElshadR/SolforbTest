using SolforbTest.Infrastructure.Db.Models;
using SolforbTest.Models;

namespace SolforbTest.Infrastructure.Services.Contracts
{
    public interface IOrderService
    {
        Task<Order> Get(int id);
        Task<IEnumerable<Order>> GetList(DateTime startDate, DateTime endDate);
        Task<Order> Create(CreateOrderModel model);
        Task<Order> Edit(EditOrderModel model);
        Task<IEnumerable<Order>> Search(SearchOrderModel model);
        Task<Order> Remove(int id);
    }
}
