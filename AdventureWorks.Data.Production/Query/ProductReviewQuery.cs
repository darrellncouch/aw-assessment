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
    public class ProductReviewQuery : IProductReviewQuery
    {
        private readonly AdventureWorks2022Context context;

        public ProductReviewQuery(AdventureWorks2022Context context) 
        {
            this.context = context;
        }

        public async Task<Result<List<ProductReview>>> GetByProductId(int productId)
        {
            try
            {
                var result = await context.ProductReviews
                    .Where(x => x.ProductId == productId)
                    .ToListAsync();

                return Result.Ok(result);
            }
            catch(Exception ex)
            {
                return Result.Fail<List<ProductReview>>(ex.Message);
            }
        }
    }
}
