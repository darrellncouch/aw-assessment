export interface IProduct {
  productId: number;
  name: string;
  productNumber: string;
  makeFlag: boolean;
  finishedGoodFlag: boolean;
  color: string;
  safetyStockLevel: number;
  reorderPoint: number;
  standardCost: number;
  listPrice: number;
  size: string;
  sizeUnitMeasureCode: string;
  weightUnitMeasureCode: string;
  weight: number;
  daysToManufacture: number;
  class: string;
  style: string;
  productSubcategoryId: number;
  productModelId: number;
  sellStartDate: Date;
  discontinuedDate: Date;
  rowGuid: string;
  ModifiedDate: Date;
}
