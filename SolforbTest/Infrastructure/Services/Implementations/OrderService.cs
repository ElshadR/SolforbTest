using Microsoft.EntityFrameworkCore;
using SolforbTest.Infrastructure.Db;
using SolforbTest.Infrastructure.Db.Models;
using SolforbTest.Infrastructure.Services.Contracts;
using SolforbTest.Models;

namespace SolforbTest.Infrastructure.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private SolforbDbContext _db { get; set; }

        public OrderService(SolforbDbContext db)
        {
            _db = db;
            DefaultData.Seed(db);
        }
        public async Task<Order> Get(int id)
        {
            var order = await _db.Orders
               .Include(x => x.OrderItems)
               .Include(x => x.Provider)
               .FirstOrDefaultAsync(x => x.Id == id);

            return order;
        }
        public async Task<IEnumerable<Order>> GetList(DateTime startDate, DateTime endDate)
        {
            var orders = await _db.Orders
                .Include(x => x.OrderItems)
                .Include(x => x.Provider)
                .Where(x => x.Date > startDate)
                .Where(x => x.Date < endDate)
                .ToListAsync();

            return orders;
        }
        public async Task<Order> Create(CreateOrderModel model)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                var order = new Order()
                {
                    Number = model.Number,
                    Date = model.Date,
                    ProviderId = model.ProviderId,
                };
                _db.Orders.Add(order);
                await _db.SaveChangesAsync();

                foreach (var item in model.OrderItems)
                {
                    var orderItem = new OrderItem()
                    {
                        OrderId = order.Id,
                        Name = item.Name,
                        Quantity = item.Quantity,
                        Unit = item.Unit,
                    };
                    _db.OrderItems.Add(orderItem);
                }
                await _db.SaveChangesAsync();

                transaction.Commit();
                return order;
            }
            catch (Exception e)
            {
                transaction.Rollback();
            }
            return null;
        }
        public async Task<Order> Edit(EditOrderModel model)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                var order = await Get(model.Id);

                if (order.Number != model.Number)
                    order.Number = model.Number;
                if (order.Date != model.Date)
                    order.Date = model.Date;
                if (order.ProviderId != model.ProviderId)
                    order.ProviderId = model.ProviderId;
                await _db.SaveChangesAsync();

                foreach (var item in order.OrderItems)
                {
                    var orderItem = model.OrderItems.FirstOrDefault(x => x.Id == item.Id);
                    if (orderItem == null)
                    {
                        _db.OrderItems.Remove(item);
                        continue;
                    }
                    if (item.Unit != orderItem.Unit)
                        item.Unit = orderItem.Unit;
                    if (item.Quantity != orderItem.Quantity)
                        item.Quantity = orderItem.Quantity;
                    if (item.Name != orderItem.Name)
                        item.Name = orderItem.Name;
                }
                foreach (var item in model.OrderItems.Where(x => x.Id == 0))
                {
                    var orderItem = new OrderItem()
                    {
                        OrderId = order.Id,
                        Name = item.Name,
                        Quantity = item.Quantity,
                        Unit = item.Unit,
                    };
                    _db.OrderItems.Add(orderItem);
                }
                await _db.SaveChangesAsync();


                transaction.Commit();
                return order;
            }
            catch (Exception e)
            {
                transaction.Rollback();
            }
            return null;
        }
        public async Task<IEnumerable<Order>> Search(SearchOrderModel model)
        {
            var orders = await _db.Orders
                .Include(x => x.OrderItems)
                .Include(x => x.Provider)
                .Where(x => model.StartDate != DateTime.MinValue ? x.Date > model.StartDate : x.Date > DateTime.Now)
                .Where(x => model.EndDate != DateTime.MinValue ? x.Date < model.EndDate : x.Date < DateTime.Now.AddMonths(1))
                .Where(x => model.Number != null ? x.Number == model.Number : true)
                .Where(x => model.ProviderId != 0 ? x.ProviderId == model.ProviderId : true)
                .Where(x => model.ProviderName != null ? x.Provider != null ? x.Provider.Name == model.ProviderName : true : true)
                .Where(x => model.OrderItemName != null ? x.OrderItems != null ? x.OrderItems.Any(x => x.Name == model.OrderItemName) : true : true)
                .Where(x => model.OrderItemUnit != null ? x.OrderItems != null ? x.OrderItems.Any(x => x.Name == model.OrderItemUnit) : true : true)
                .ToListAsync();

            return orders;
        }
        public async Task<Order> Remove(int id)
        {
            try
            {
                var order = await Get(id);
                var removeOrder = _db.Remove(order);
                await _db.SaveChangesAsync();
                return order;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
