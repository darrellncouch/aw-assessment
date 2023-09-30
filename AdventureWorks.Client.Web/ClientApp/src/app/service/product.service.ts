import { NumberSymbol } from '@angular/common';
import { IPagedRequest } from './../core/IPagedRequest';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPagedResult } from '../core/IPagedResult';
import { IProduct } from '../core/IProduct';
import { IProductSummary } from '../core/IProductSummary';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private readonly apiUrl = "https://localhost:7001";
  constructor(private readonly httpClient: HttpClient) { }

  public getProducts(page: number, size: number): Observable<IPagedResult<IProductSummary>> {
    return this.httpClient.get<IPagedResult<IProductSummary>>(`${this.apiUrl}/product`, {
      params: new HttpParams().set("pageSize", size).set("pageNumber", page)
    })
  }
}
