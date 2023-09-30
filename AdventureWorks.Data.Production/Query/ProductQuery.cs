using AdventureWorks.Data.Common;
using AdventureWorks.Data.Production.EntityFramework;
using AdventureWorks.Data.Production.Interfaces.Query;
using Microsoft.EntityFrameworkCore;
namespace AdventureWorks.Data.Production.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly AdventureWorks2022Context context;

        public ProductQuery(AdventureWorks2022Context context)
        {
            this.context = context;
        }

        public async Task<Result<Product>> GetProductById(int productId)
        {
            try
            {
                var result = await context.Products.SingleAsync(p => p.ProductId == productId);
                return Result.Ok(result);
            }
            catch(Exception ex)
            {
                return Result.Fail<Product>(ex.Message);
            }
        }

        public async Task<Result<int>> GetProductCount()
        {
            try
            {
                return Result.Ok(await context.Products.CountAsync());
            }
            catch (Exception ex) 
            {
                return Result.Fail<int>(ex.Message);
            }
        }

        public async Task<Result<PagedResult<Product>>> GetProducts(PagedRequest req)
        {
            try
            {
                var result = await context.Products
                    .OrderBy(p => p.ProductId)
                    .Skip(req.PageSize * (req.PageIndex))
                    .Take(req.PageSize)
                    .ToListAsync();

                return Result.Ok(new PagedResult<Product>
                { 
                    PageIndex = req.PageIndex,
                    PageSize = req.PageSize,
                    Value = result
                });
            }
            catch(Exception ex) 
            {
                return Result.Fail<PagedResult<Product>>(ex.Message);
            }
        }

    }
}
