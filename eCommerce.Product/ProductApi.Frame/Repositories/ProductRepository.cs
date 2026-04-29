using eCommerce.SharedLibary.Logs;
using eCommerce.SharedLibary.Response;
using Microsoft.EntityFrameworkCore;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using ProductApi.Frame.Data;
using System;
using System.Linq.Expressions;

namespace ProductApi.Frame.Repositories
{
    public class ProductRepository(ProductDbContext context) : IProduct
    {
        public async Task<Response> CreateAsync(Product entity)
        {
            try
            {
                //Check if the product already exist
                var getProdct = await GetByAsync(_ => _.Name!.Equals(entity.Name));
                if (getProdct is not null && !string.IsNullOrEmpty(getProdct.Name))
                    return new Response(false, $"{entity.Name} already exist");
                var currentEntity = context.Products.Add(entity).Entity;
                await context.SaveChangesAsync();
                if (currentEntity is not null && currentEntity.Id > 0)
                    return new Response(true, $"{entity.Name} added to database successfully");
                else
                    return new Response(false, "Error occurred while adding {entity.Name}");

            }
            catch (Exception ex)
            {
                //Log the original exception
                LogException.LogExceptions(ex);

                //display scary-free message to the client
                return new Response(false, "Error occurred adding new product");

               
            }
            }
            

        public async Task<Response> DeleteAsync(Product entity)
        {
            try
            {
                var product = await FindByIdAsync(entity.Id);
                if (product is null)
                    return new Response(false, $"{entity.Name} not found");

                context.Products.Remove(product);
                await context.SaveChangesAsync();
                return new Response(false, $"{entity.Name} is deleted succesfully");


            }
            catch (Exception ex)
            {
                //Log the original exception
                LogException.LogExceptions(ex);

                //display scary-free message to the client
                return new Response(false, "Error deleting product");


            }
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            try
            {
                var product = await context.Products.FindAsync(id);
                return product is not null ? product : null!;
            }
            catch (Exception ex)
            {
                //Log the original exception
                LogException.LogExceptions(ex);

                //display scary-free message to the client
                throw new Exception("Error occurred while retriving product");


            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                var products = await context.Products.AsNoTracking().ToListAsync();
                return products is not null ? products : null!;

            }
            catch (Exception ex)
            {
                //Log the original exception
                LogException.LogExceptions(ex);

                //display scary-free message to the client
                throw new InvalidOperationException("Error occurred while retriving product");


            }
        }

        public async Task<Product> GetByAsync(Expression<Func<Product, bool>> predicate)
        {
            try 
            {
            var product = await context.Products.Where(predicate).FirstOrDefaultAsync()!;
            return product is not null ? product : null!;

        }
        catch (Exception ex)
        {
                //Log original exception
                LogException.LogExceptions(ex);

                //display scary-free message to the client
                throw new InvalidOperationException("Error occurred while retriving product");

    }
}

public async Task<Response> UpdateAsync(Product entity)
        {
            try
            {
                var product = await FindByIdAsync(entity.Id);
                if (product is null) 
                    return new Response(false, $"{entity.Name} not found");

                context.Entry(product).State = EntityState.Detached;
                context.Products.Update(entity);
                await context.SaveChangesAsync();
                return new Response(true, $"{entity.Name} is updated successfully");
            }
            catch (Exception ex)
            {
                //Log the original exception
                LogException.LogExceptions(ex);

                //Display scary-free message to the client
                return new Response(false,"Error occurred while updating existing product");


            }
        }
    }
}
