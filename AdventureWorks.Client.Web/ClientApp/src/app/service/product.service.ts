import { IProductInformation } from 'src/core/IProductInformation';
import { IProduct } from 'src/core/IProduct';
import { HttpClient, HttpErrorResponse, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IPagedResult } from '../../core/IPagedResult';
import { IProductSummary } from '../../core/IProductSummary';
import { IProductModel } from '../../core/IProductModel';
import { IValidation } from 'src/core/IValidation';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private targetProduct = new BehaviorSubject<IProductInformation>({} as IProductInformation);
  public _targetProduct = this.targetProduct.asObservable();

  public updateTarget(product: IProductInformation)
  {
    this.targetProduct.next(product);
  }

  private readonly apiUrl = "https://localhost:7001/product";
  constructor(private readonly httpClient: HttpClient) { }

  public validate(product: IProduct): IValidation {
    const retObj: IValidation =
    {
      isValid: true,
      error: ""
    }
    if(product.name == "") {
      retObj.isValid = false;
      retObj.error += "Product Name is required. "
    }

    if(product.productNumber == "") {
      retObj.isValid = false;
      retObj.error += "Product Number is required. "
    }

    if(product.safetyStockLevel == 0) {
      retObj.isValid = false;
      retObj.error += "Safety Stock Level cannot be 0. "
    }

    return retObj;
  }

  public getProducts(page: number, size: number): Observable<IPagedResult<IProductSummary>> {
    return this.httpClient.get<IPagedResult<IProductSummary>>(`${this.apiUrl}`, {
      params: new HttpParams().set("pageSize", size).set("pageNumber", page)
    })
  }

  public getProductInfoById(id: number): Observable<IProductInformation> {
    return this.httpClient.get<IProductInformation>(`${this.apiUrl}/${id}`);
  }

  public addProduct(product: IProduct): Observable<number> {
    return this.httpClient.post<number>(this.apiUrl, product)
  }

  public getAllProductModels(): Observable<Array<IProductModel>> {
    return this.httpClient.get<Array<IProductModel>>(`${this.apiUrl}/model`);
  }

  public getProductModelById(id: number): Observable<IProductModel> {
    return this.httpClient.get<IProductModel>(`${this.apiUrl}/model/${id}`);
  }

  public updateProduct(product: IProduct): Observable<boolean> {
    return this.httpClient.put<boolean>(this.apiUrl, product);
  }
}
