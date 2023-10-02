using AdventureWorks.Data.Common;
using AdventureWorks.Data.DTO;
using AdventureWorks.Data.Production.EntityFramework;
using AdventureWorks.Data.Production.Interfaces.Query;
using AdventureWorks.Data.Production.Interfaces.Repository;
using AdventureWorks.Logic.Core.Interfaces;
using AdventureWorks.Web.ServiceInterfaces;

namespace AdventureWorks.Logic.Core
{
    public class ProductManager : LogicBase, IProductManager
    {
        private readonly IProductQuery query;
        private readonly IProductModelQuery modelQuery;
        private readonly IProductReviewQuery reviewQuery;
        private readonly IProductRepository productRepo;
        private readonly string GenericErrorMessage = "Unexpected error. Please try again or reach out to customer service.";

        public ProductManager(
            IProductQuery query,
            IProductModelQuery modelQuery,
            IProductReviewQuery reviewQuery,
            IProductRepository productRepo
        )
        {
            this.query = query;
            this.modelQuery = modelQuery;
            this.reviewQuery = reviewQuery;
            this.productRepo = productRepo;
        }

        public async Task<Result<int>> AddProduct(ProductDto product)
        {
            product.Rowguid = Guid.NewGuid();

            var result = await productRepo.Create(Mapper.Map<Product>(product));


            if (result.IsFailure)
            {
                LogError("ProductManager/AddProduct", result.Error);
                return Result.Fail<int>(GenericErrorMessage);
            }

            return Result.Ok(result.Value);
        }

        public async Task<Result<List<ProductModelDto>>> GetAllModels()
        {
            var result = await modelQuery.GetAll();

            if(result.IsFailure)
            {
                LogError("ProductManager/GetAllModels", result.Error);
                return Result.Fail<List<ProductModelDto>>(GenericErrorMessage);
            }

            return Result.Ok(Mapper.Map<List<ProductModelDto>>(result.Value));
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

            productInfo.AverageRating = productInfo.Reviews.Count() == 0 ? null : productInfo.Reviews.Select(r => r.Rating).ToList().Sum() / productInfo.Reviews.Count();

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

        public async Task<Result<PagedResult<ProductSummaryDto>>> GetProductSummaries(PagedRequest req)
        {
            var countResult = await query.GetProductCount();

            if (countResult.IsFailure)
            {
                LogError("ProductManager/GetProductSummaries", countResult.Error);
                return Result.Fail<PagedResult<ProductSummaryDto>>("Unexpected error. Please try again or reach out to customer service.");
            }

            var hasNextPage = (int pageNumber) => countResult.Value - req.PageSize * (pageNumber) > 0;

            if(req.PageIndex != 0 && !hasNextPage(req.PageIndex))
            {
                return Result.Fail<PagedResult<ProductSummaryDto>>("End of results");
            }

            var productResult = await query.GetProducts(req);

            if (productResult.IsFailure)
            {
                LogError("ProductManager/GetProductSummaries", productResult.Error);
                return Result.Fail<PagedResult<ProductSummaryDto>>(GenericErrorMessage);
            }

            var summary = productResult.Value.Value
                .Select(x => new ProductSummaryDto
                {
                    ProductId = x.ProductId,
                    ProductModelId = x.ProductModelId,
                    Name = x.Name,
                    ListPrice = x.ListPrice,
                }).ToList();

            foreach (var product in summary)
            {
                if (!product.ProductModelId.HasValue)
                {
                    continue;
                }

                var modelResult = await modelQuery.GetById((int)product.ProductModelId);

                if (modelResult.IsFailure)
                {
                    LogError("ProductManager/GetProductSummaries", modelResult.Error);
                    return Result.Fail<PagedResult<ProductSummaryDto>>(GenericErrorMessage);
                }

                product.ModelName = modelResult.Value.Name;
            }

            return Result.Ok(new PagedResult<ProductSummaryDto>
            {
                PageCount = Math.Ceiling((decimal)(countResult.Value / req.PageSize)),
                TotalCount = countResult.Value,
                HasNextPage = hasNextPage(req.PageIndex),
                PageIndex = req.PageIndex,
                PageSize = req.PageSize,
                Value = summary
            });
        }

        //TODO: maybe useful if not then remove
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

        public async Task<Result> UpdateProduct(ProductDto product)
        {

            var result = await productRepo.Update(Mapper.Map<Product>(product));

            if(result.IsFailure) 
            {
                LogError("ProductManager/UpdateProducts", result.Error);
                return Result.Fail(GenericErrorMessage);
            }

            return Result.Ok();
        }

        private void LogError(string origin, string error)
        {
            Console.WriteLine($"{DateTimeOffset.UtcNow} - {origin}: {error}");
        }

        public async Task<Result<ProductModelDto>> GetModelById(int id)
        {
            var result = await modelQuery.GetById(id);

            if(result.IsFailure) 
            {
                LogError("ProductManager/GetModelById", result.Error);
                return Result.Fail<ProductModelDto>(GenericErrorMessage);
            }

            return Result.Ok(Mapper.Map<ProductModelDto>(result.Value));
        }
    }
}
