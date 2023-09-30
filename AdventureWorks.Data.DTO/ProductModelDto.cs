using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Data.DTO
{
    public class ProductModelDto
    {
        public int ProductModelId { get; set; }

        public string Name { get; set; } = null!;

        public string? CatalogDescription { get; set; }

        public string? Instructions { get; set; }

        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
