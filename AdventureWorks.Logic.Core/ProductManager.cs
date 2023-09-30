using AdventureWorks.Data.Common;
using AdventureWorks.Data.DTO;
using AdventureWorks.Data.Production.Interfaces.Query;
using AdventureWorks.Logic.Core.Interfaces;
using AdventureWorks.Web.ServiceInterfaces;

namespace AdventureWorks.Logic.Core
{
    public class ProductManager : LogicBase, IProductManager
    {
        private readonly IProductQuery query;
        private readonly IProductModelQuery modelQuery;
        private readonly IProductReviewQuery reviewQuery;
        private readonly string GenericErrorMessage = "Unexpected error. Please try again or reach out to customer service.";

        public ProductManager(
            IProductQuery query,
            IProductModelQuery modelQuery,
            IProductReviewQuery reviewQuery
        )
        {
            this.query = query;
            this.modelQuery = modelQuery;
            this.reviewQuery = reviewQuery;
        }

        public Task<Result<int>> AddProduct(ProductDto product)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<ProductInformationDto>> GetInfoById(int productId)
        {
            var detailsResult = await query.GetProductById(productId);
            
            if(detailsResult.IsFailure)
            {
                LogError("ProductManager/GetInfoById", detailsResult.Error);
                return Result.Fail<ProductInformationDto>(GenericErrorMessage);
            }

            ProductDto details = Mapper.Map<ProductDto>(detailsResult.Value);
            var reviewsResult = await reviewQuery.GetByProductId(productId);

            if(reviewsResult.IsFailure)
            {
                LogError("ProductManager/GetInfoById", reviewsResult.Error);
                return Result.Fail<ProductInformationDto>(GenericErrorMessage);
            }

            var productInfo = new ProductInformationDto(details, Mapper.Map<List<ProductReviewDto>>(reviewsResult.Value));

            if(details.ProductModelId.HasValue) 
            {
                var modelResult = await modelQuery.GetById(details.ProductModelId.Value);

                if(modelResult.IsFailure) 
                {
                    LogError("ProductManger/GetInfoById", modelResult.Error);
                }
                else
                {
                    productInfo.Model = Mapper.Map<ProductModelDto>(modelResult.Value);
                }
            }

            return Result.Ok(productInfo);
        }

        public async Task<Result<PagedResult<ProductDto>>> GetProducts(PagedRequest req)
        {
            var countResult = await query.GetProductCount();

            if(countResult.IsFailure)
            {
                LogError("ProductManager/GetProducts", countResult.Error);
                return Result.Fail<PagedResult<ProductDto>>("Unexpected error. Please try again or reach out to customer service.");
            }

            var hasNextPage = (int pageNumber) => countResult.Value - req.PageSize * (pageNumber) > 0;

            if(req.PageIndex != 0 && !hasNextPage(req.PageIndex)) 
            {
                return Result.Fail<PagedResult<ProductDto>>("End of results");
            }

            var result = await query.GetProducts(req);

            if(result.IsFailure) 
            {
                LogError("ProductManager/GetProducts", result.Error);
                return Result.Fail<PagedResult<ProductDto>>("Unexpected error. Please try again or reach out to customer service.");
            }

            var pagedProduct = Mapper.Map<PagedResult<ProductDto>>(result.Value);
            pagedProduct.PageIndex = req.PageIndex;
            pagedProduct.HasNextPage = hasNextPage(pagedProduct.PageIndex);
            pagedProduct.PageCount = Math.Ceiling((decimal)(countResult.Value / req.PageSize));
            pagedProduct.TotalCount = countResult.Value;

            return Result.Ok(pagedProduct);
        }

        public Task<Result> UpdateProduct(ProductDto product)
        {
            throw new NotImplementedException();
        }

        private void LogError(string origin, string error)
        {
            Console.WriteLine($"{DateTimeOffset.UtcNow} - {origin}: {error}");
        }
    }
}
