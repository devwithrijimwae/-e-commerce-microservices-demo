using eCommerce.SharedLibary.Response;
using System.Linq.Expressions;
namespace eCommerceSharedLibary.Interface
{
    public interface IGenericIterface <T> where T : class
    { 
        Task<Response>CreateAsync(T entity);
        Task<Response>UpdateAsync(T entity);
        Task<Response>DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindByIdAsync(int id);
        Task<T> GetByAsync(Expression<Func<T, bool>> predicate);
    }
}
