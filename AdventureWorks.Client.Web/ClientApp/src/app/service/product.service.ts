import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { IPagedResult } from '../core/IPagedResult';
import { IProductSummary } from '../core/IProductSummary';
import { IProductInformation } from '../core/IProductInformation';

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

  private readonly apiUrl = "https://localhost:7001";
  constructor(private readonly httpClient: HttpClient) { }

  public getProducts(page: number, size: number): Observable<IPagedResult<IProductSummary>> {
    return this.httpClient.get<IPagedResult<IProductSummary>>(`${this.apiUrl}/product`, {
      params: new HttpParams().set("pageSize", size).set("pageNumber", page)
    })
  }

  public getProductById(id: number): Observable<IProductInformation> {
    return this.httpClient.get<IProductInformation>(`${this.apiUrl}/product/${id}`);
  }
}
