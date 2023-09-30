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
    }
}
