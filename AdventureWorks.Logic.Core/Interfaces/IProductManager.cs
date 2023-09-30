using AdventureWorks.Data.Common;
using AdventureWorks.Data.DTO;

namespace AdventureWorks.Logic.Core.Interfaces
{
    public interface IProductManager
    {
        Task<Result<PagedResult<ProductInformationDto>>> GetProducts(PagedRequest req);
        Task<Result<int>> AddProduct(ProductDto product);
        Task<Result> UpdateProduct(ProductDto product);
        Task<Result<ProductInformationDto>> GetInfoById(int productId);
    }
}
