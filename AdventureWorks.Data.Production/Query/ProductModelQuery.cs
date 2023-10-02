using AdventureWorks.Data.Common;
using AdventureWorks.Data.Production.EntityFramework;
using AdventureWorks.Data.Production.Interfaces.Query;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Data.Production.Query
{
    public  class ProductModelQuery : IProductModelQuery
    {
        private readonly AdventureWorks2022Context context;

        public ProductModelQuery(AdventureWorks2022Context context) 
        {
            this.context = context;
        }

        public async Task<Result<ProductModel>> GetById(int id)
        {
            try
            {
                var result = await context.ProductModels.SingleAsync(x => x.ProductModelId == id);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail<ProductModel>(ex.Message);
            }
        }

        public async Task<Result<List<ProductModel>>> GetAll()
        {
            try
            {
                var result = await context.ProductModels.OrderBy(x => x.Name).ToListAsync();
                return Result.Ok(result);
            }
            catch(Exception ex)
            {
                return Result.Fail<List<ProductModel>>(ex.Message);
            }
        }
    }
}
