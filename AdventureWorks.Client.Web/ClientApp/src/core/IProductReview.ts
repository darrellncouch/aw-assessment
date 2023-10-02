export interface IProductReview {
  productReviewId: number;
  productId: number;
  reviewerName: string;
  reviewDate: Date;
  emailAddress: string;
  rating: number;
  comments: string;
  modifiedDate: Date;
}
