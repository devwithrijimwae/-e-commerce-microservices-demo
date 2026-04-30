using Microsoft.EntityFrameworkCore;
using ProductApi.Frame.Data;
using ProductApi.Frame.Repositories;

namespace UnitTest.Product.Repositories
{
    public class ProductRepositoryTest
    {
        private readonly ProductDbContext productDbContext;
        private readonly ProductRepository productRepository;

        public ProductRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(databaseName: "TestProductDb")
                .Options;
        }
    }
}
