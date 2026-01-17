using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Interfaces.Cart;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Infrastructure.Repositories.Cart
{
    public class CartRepository(AppDbContext context) : ICart
    {
        public async Task<IEnumerable<Achieve>> GetAllCheckoutHistory()
        {
            return await context.ChecoutAchieves.AsNoTracking().ToListAsync();
        }

        public async Task<int> SaveCheckoutHistory(IEnumerable<Achieve> checkouts)
        {
            context.ChecoutAchieves.AddRange(checkouts);
            return await context.SaveChangesAsync();
        }
    }
}
