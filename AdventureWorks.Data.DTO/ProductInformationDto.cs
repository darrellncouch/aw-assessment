namespace AdventureWorks.Data.DTO
{
    public class ProductInformationDto
    {
        public ProductDto Details { get; set; } = default!;
        public List<ProductReviewDto> Reviews { get; set; } = default!;
        public ProductModelDto? Model { get; set; }
        public decimal? AverageRating { get; set; }

        public ProductInformationDto(ProductDto details, List<ProductReviewDto> reviews) 
        { 
            Details = details;
            Reviews = reviews;
        }
    }
}
