using AdventureWorks.Data.Common;
using AdventureWorks.Data.Production.EntityFramework;

namespace AdventureWorks.Data.Production.Interfaces.Query
{
    public interface IProductReviewQuery
    {
        Task<Result<List<ProductReview>>> GetByProductId(int productId);
    }
}
