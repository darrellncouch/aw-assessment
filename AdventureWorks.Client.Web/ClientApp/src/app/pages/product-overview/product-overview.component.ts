import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { IProductInformation } from 'src/core/IProductInformation';
import { ProductService } from 'src/app/service/product.service';
import { Product } from 'src/core/Product';

@Component({
  selector: 'app-product-overview',
  templateUrl: './product-overview.component.html',
  styleUrls: ['./product-overview.component.scss']
})
export class ProductOverviewComponent {

  public product: IProductInformation = {} as IProductInformation;
  public id: number = 0;
  private unsubAll: Subject<any>;

  constructor(
    private readonly activatedRouter: ActivatedRoute,
    private readonly productService: ProductService,
    private readonly router: Router
  )
  {
    this.unsubAll = new Subject();
    this.product.details = new Product()
  }

  ngOnInit(): void {
    this.productService._targetProduct
      .pipe(takeUntil(this.unsubAll))
      .subscribe(x => this.product = x);

    this.activatedRouter.queryParams
      .subscribe(param => {
        if(param.id == undefined)
        {
          return;
        }
        this.id = parseInt(param.id);
      })

    this.productService.getProductInfoById(this.id)
      .pipe(takeUntil(this.unsubAll))
      .subscribe(p => {
        this.productService.updateTarget(p);
      })
  }

  public updateClickHandler(): void {
    this.router.navigate(['products/edit'], {queryParams : {id: this.id}});
  }
}
