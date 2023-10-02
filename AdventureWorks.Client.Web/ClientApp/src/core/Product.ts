import { IProduct } from "./IProduct";
export class Product implements IProduct {
  public productId: number = 0;
  public name: string = "";
  public productNumber: string = "";
  public makeFlag: boolean = false;
  public finishedGoodsFlag: boolean = false;
  public color?: string;
  public safetyStockLevel: number = 0;
  public reorderPoint: number = 0;
  public standardCost: number = 0.00;
  public listPrice: number = 0.00;
  public size?: string;
  public sizeUnitMeasureCode?: string;
  public weightUnitMeasureCode?: string;
  public weight?: number;
  public daysToManufacture: number = 0;
  public class?: string;
  public style?: string;
  public productSubcategoryId?: number;
  public productModelId?: number;
  public sellStartDate: Date = new Date(Date.now());
  public sellEndDate?: Date;
  public discontinuedDate?: Date;
  public rowGuid?: string;
  public modifiedDate: Date = new Date(Date.now());

}
