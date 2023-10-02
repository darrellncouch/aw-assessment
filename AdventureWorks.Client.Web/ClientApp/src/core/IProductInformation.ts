import { IProduct } from "./IProduct";
import { IProductModel } from "./IProductModel";
import { IProductReview } from "./IProductReview";

export interface IProductInformation {
  details: IProduct,
  reviews: Array<IProductReview>,
  model: IProductModel
  averageRating: number
}
