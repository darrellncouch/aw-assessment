using AdventureWorks.Data.Common;
using AdventureWorks.Data.DTO;

namespace AdventureWorks.Logic.Core.Interfaces
{
    public interface IProductManager
    {
        Task<Result<int>> AddProduct(ProductDto product);
        Task<Result<ProductInformationDto>> GetInfoById(int productId);
        Task<Result<PagedResult<ProductDto>>> GetProducts(PagedRequest req);
        Task<Result<PagedResult<ProductSummaryDto>>> GetProductSummaries(PagedRequest req);
        Task<Result> UpdateProduct(ProductDto product);
    }
}
