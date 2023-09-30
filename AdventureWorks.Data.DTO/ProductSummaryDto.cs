using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Data.DTO
{
    public class ProductSummaryDto
    {
        public int ProductId { get; set; }
        public int? ProductModelId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ModelName { get; set; } = string.Empty;
        public decimal ListPrice { get; set; }
    }
}
