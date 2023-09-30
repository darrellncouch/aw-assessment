using AdventureWorks.Data.Common;
using AdventureWorks.Data.Production.EntityFramework;

namespace AdventureWorks.Data.Production.Interfaces.Query
{
    public interface IProductQuery
    {
        Task<Result<PagedResult<Product>>> GetProducts(PagedRequest req);
        Task<Result<int>> GetProductCount();
        Task<Result<Product>> GetProductById(int productId);
    }
}
