using AdventureWorks.Data.Common;
using AdventureWorks.Data.Production.EntityFramework;
using AdventureWorks.Data.Production.Interfaces.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
