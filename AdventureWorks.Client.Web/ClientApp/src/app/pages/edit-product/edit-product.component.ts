import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subject, catchError, map, of, takeUntil } from 'rxjs';
import { ProductService } from 'src/app/service/product.service';
import { Product } from 'src/core/Product';
import { IProduct } from 'src/core/IProduct';
import { UiService } from 'src/app/service/ui.service';
import { ToastType } from 'src/core/IToast';
import { HttpErrorResponse } from '@angular/common/http';
import { SizeUnits } from 'src/core/SizeUnits';
import { WeightUnits } from 'src/core/WeightUnits';
import { IProductModel } from 'src/core/IProductModel';

@Component({
  selector: 'edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.scss']
})
export class EditProductComponent {

  public product: IProduct = new Product();
  public id: number = 0;
  private unsubAll: Subject<any>;
  public sizeUnits = SizeUnits;
  public weightUnits = WeightUnits;
  public productModels: Array<IProductModel> = [];
  public selectedProductModel: IProductModel = {} as IProductModel;

  constructor(
    private readonly activatedRouter: ActivatedRoute,
    private readonly productService: ProductService,
    private readonly uiService: UiService,
    private readonly router: Router
  )
  {
    this.unsubAll = new Subject();
  }

  ngOnInit(): void {
    this.productService._targetProduct
      .pipe(takeUntil(this.unsubAll))
      .subscribe(x => this.product = x.details)

      console.log(this.product)

    this.activatedRouter.queryParams
      .subscribe(param => {
        if(param.id == undefined || parseInt(param.id) == Number.NaN)
        {
          this.uiService.showToast(ToastType.error, "Invalid url")
          return;
        }
        this.id = parseInt(param.id);
      });

      if(this.product == undefined  || this.product.productId == 0 || this.product.productId != this.id)
      {
        this.productService.getProductInfoById(this.id)
          .pipe(takeUntil(this.unsubAll))
          .subscribe(p => {
            this.productService.updateTarget(p);
        });
      }

      this.productService.getAllProductModels()
        .pipe(takeUntil(this.unsubAll))
        .subscribe(models => this.productModels = models);

      if(this.product.productModelId && this.product.productModelId !== 0)
      {
        this.productService.getProductModelById(this.product.productModelId)
          .pipe(takeUntil(this.unsubAll))
          .subscribe(pm => this.selectedProductModel = pm)
      }
  }

  public productModelChangeHandler(id: number): void {
    this.product.productModelId = id;
    this.selectedProductModel = this.productModels[this.productModels.map(x => x.productModelId).indexOf(id)]
  }

  public save(): void {
    const validationResult = this.productService.validate(this.product);

    if(!validationResult.isValid){
      this.uiService.showToast(ToastType.error, validationResult.error);
      return;
    }

    this.productService.updateProduct(this.product)
    .pipe(
      takeUntil(this.unsubAll),
      catchError(
        (err : HttpErrorResponse ): Observable<boolean> => {
          this.uiService.showToast(ToastType.error, err.error);
          console.log(err)
          return of(false);
      }),
      map(result => {
        if(result) {
          this.uiService.showToast(ToastType.info, "Successully updated product");
          setTimeout(
            () => this.router.navigate(['product-view'], {queryParams: {id: this.id}}),
            1000
          );
        }
      })
      ).subscribe();
  }

  ngOnDestroy(): void {
    this.unsubAll.unsubscribe();
  }
}
