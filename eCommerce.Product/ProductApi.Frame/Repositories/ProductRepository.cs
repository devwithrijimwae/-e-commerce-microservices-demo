using eCommerce.SharedLibary.Logs;
using eCommerce.SharedLibary.Response;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entity;
using ProductApi.Frame.Data;
using System.Linq.Expressions;

namespace ProductApi.Frame.Repositories
{
    public class ProductRepository(ProductDbContext context) : IProduct
    {
        public async Task<Response> CreateAsync(Product entity)
        {
            try
            {
                var getProdct = await GetByAsync(_ => _.Name!.Equals(entity.Name));
                if (getProdct is not null && !string.IsNullOrEmpty(getProdct.Name))
                    return new Response(false, $"{entity.Name} already exist");


            }catch (Exception ex)
            {
                //Log the original exception
                LogException.LogExceptions(ex);

                //display scary-free message to the client
                return new Response(false, "Error occurred adding new product");
            }
            }
            

        public Task<Response> DeleteAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<Product> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByAsync(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
