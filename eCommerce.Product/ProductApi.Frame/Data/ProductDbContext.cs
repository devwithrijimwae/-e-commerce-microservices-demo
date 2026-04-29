using Microsoft.EntityFrameworkCore;
using ProductApi.Domain.Entities;

namespace ProductApi.Frame.Data
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options):DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
    }
}
