import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import {MatTableModule} from '@angular/material/table';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { IProduct } from 'src/core/IProduct';
import { IProductSummary } from 'src/core/IProductSummary';
import { PagedRequest } from 'src/core/PagedRequest';
import { ProductService } from 'src/app/service/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent {
  displayedColumns: string[] = ['productId', 'name'];
  dataSource: Array<IProductSummary>  = [] as Array<IProductSummary>;

  public page: number = 0;
  public size: number = 20;
  public count: number = 0;
  public hasNextpage: boolean = false;

  private unsubAll: Subject<any>;

  constructor(
    private readonly productService: ProductService,
    private readonly router: Router
  ) {
    this.unsubAll = new Subject();
  }

  ngOnInit(): void {
    this.productService.getProducts(this.page, this.size)
        .pipe(takeUntil(this.unsubAll))
        .subscribe(x => {
          this.dataSource = x.value;
          this.count = x.totalCount;
          this.hasNextpage = x.hasNextPage;
        })
  }

  public handlePageEvent(e: PageEvent) {
    this.size = e.pageSize;
    this.page = e.pageIndex;
    this.productService.getProducts(this.page, this.size)
      .pipe(takeUntil(this.unsubAll))
      .subscribe(x => {
        this.dataSource = x.value;
        this.count = x.totalCount;
        this.hasNextpage = x.hasNextPage;
      })
  }

  public onViewClick(id: number) {
    this.router.navigate(['/product-view'], {queryParams: {id: id}});
  }

  public onEditClick(id: number) {
    this.router.navigate(['/products/edit'], {queryParams: {id: id}});
  }
}
