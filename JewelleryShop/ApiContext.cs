using Microsoft.EntityFrameworkCore;
using JewelleryShop.DbModels;

namespace JewelleryShop
{
    public class ApiContext : DbContext
    {
        
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

    }
}