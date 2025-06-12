using AvdoshkaMMM.Domain.Entities;
using AvdoshkaMMM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AvdoshkaMMM.Infrastructure.Repository
{
    public class OrderRepository
    {
        private readonly AvdoshkaMMMDbContext db;
        private readonly ILogger<OrderRepository> logger;
        public OrderRepository(AvdoshkaMMMDbContext _db, ILogger<OrderRepository> _logger)
        {
            db = _db;
            logger = _logger;
        }

        //Просмотр всех заказов с содержимым как админ
        public async Task<IEnumerable<Order>> GetOrders()
        {
            var orders = await db.Orders.Include(o => o.OrderProducts).ThenInclude(p => p.Product).Select(u => u.UserID).ToListAsync();
        }
    }
}
