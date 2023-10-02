using AdventureWorks.Data.Common;
using AdventureWorks.Data.Production.EntityFramework;
using AdventureWorks.Data.Production.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Data.Production.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AdventureWorks2022Context context;

        public ProductRepository(AdventureWorks2022Context context) 
        {
            this.context = context;
        }

        public async Task<Result<int>> Create(Product product)
        {
            try
            {
                var result = await context.Products.AddAsync(product);
                await context.SaveChangesAsync();

                return Result.Ok(product.ProductId);
            }
            catch(Exception ex)
            {
                return Result.Fail<int>(ex.Message);
            }
        }

        public async Task<Result> Update(Product product)
        {
            try
            {
                context.Products.Update(product);
                await context.SaveChangesAsync();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
