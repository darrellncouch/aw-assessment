using AdventureWorks.Data.Common;
using AdventureWorks.Data.DTO;
using Microsoft.AspNetCore.Mvc;
using AdventureWorks.Logic.Core.Interfaces;

namespace AdventureWorks.Web.Api.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductManager productManager;

        public ProductController(IProductManager productManager)
        {
            this.productManager = productManager;
        }

        [HttpGet("")]
        public async Task<ActionResult<PagedResult<ProductSummaryDto>>> GetPagedProducts([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 0)
        {
            var result = await productManager.GetProductSummaries(new PagedRequest { PageSize = pageSize, PageIndex = pageNumber });

            return result.IsFailure
                ? BadRequest(result.Error)
                : Ok(result.Value);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductInformationDto>> GetProductInfo([FromRoute] int Id)
        {
            var result = await productManager.GetInfoById(Id);

            return result.IsFailure
                ? BadRequest(result.Error)
                : Ok(result.Value);
        }

        [HttpPost("")]
        public async Task<ActionResult<int>> Create([FromBody] ProductDto product)
        {
            var result = await productManager.AddProduct(product);

            return result.IsFailure
                ? BadRequest(result.Error)
                : Ok(result.Value);
        }

        [HttpPut("")]
        public async Task<ActionResult<bool>> Update([FromBody] ProductDto product)
        {
            var result = await productManager.UpdateProduct(product);

            return result.IsFailure
                ? BadRequest(result.Error)
                : Ok(true);
        }

        [HttpGet("model")]
        public async Task<ActionResult<List<ProductModelDto>>> GetAllProductModels()
        {
            var result = await productManager.GetAllModels();

            return result.IsFailure
                ? BadRequest(result.Error)
                : Ok(result.Value);
        }

        [HttpGet("model/{Id}")]
        public async Task<ActionResult<ProductModelDto>> GetProductModelById([FromRoute] int Id)
        {
            var result = await productManager.GetModelById(Id);

            return result.IsFailure
                ? BadRequest(result.Error)
                : Ok(result.Value);
        }

    }
}
