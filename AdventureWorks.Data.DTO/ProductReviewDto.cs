using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Data.DTO
{
    public class ProductReviewDto
    {
        public int ProductReviewId { get; set; }

        public int ProductId { get; set; }

        public string ReviewerName { get; set; } = null!;

        public DateTime ReviewDate { get; set; }

        public string EmailAddress { get; set; } = null!;

        public int Rating { get; set; }

        public string? Comments { get; set; }

        public DateTime ModifiedDate { get; set; }

    }
}
