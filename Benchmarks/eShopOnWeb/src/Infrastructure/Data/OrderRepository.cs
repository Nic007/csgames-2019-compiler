using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Infrastructure.Data
{
    public class OrderRepository : EfRepository<Order>, IOrderRepository
    {
        public OrderRepository(CatalogContext dbContext) : base(dbContext) // @issue@I02
        {
        }

        public Order GetByIdWithItems(int id) // @issue@I02
        {
            return _dbContext.Orders // @issue@I02
                .Include(o => o.OrderItems)
                .Include($"{nameof(Order.OrderItems)}.{nameof(OrderItem.ItemOrdered)}")
                .FirstOrDefault();
        }

        public Task<Order> GetByIdWithItemsAsync(int id) // @issue@I02
        {
            return _dbContext.Orders // @issue@I02
                .Include(o => o.OrderItems)
                .Include($"{nameof(Order.OrderItems)}.{nameof(OrderItem.ItemOrdered)}")
                .FirstOrDefaultAsync();
        }
    }
}
