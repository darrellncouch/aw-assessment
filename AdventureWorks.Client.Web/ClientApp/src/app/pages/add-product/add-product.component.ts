import { Component } from '@angular/core';
import { Subject, catchError, map, takeUntil, Observable, of } from 'rxjs';
import { IProduct } from 'src/core/IProduct';
import { IProductModel } from 'src/core/IProductModel';
import { Product } from 'src/core/Product';
import { ProductService } from 'src/app/service/product.service';
import { SizeUnits } from 'src/core/SizeUnits';
import { WeightUnits } from 'src/core/WeightUnits';
import { UiService } from 'src/app/service/ui.service';
import { ToastType } from 'src/core/IToast';
import { HttpErrorResponse } from '@angular/common/http';
import { IValidation } from 'src/core/IValidation';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.scss']
})
export class AddProductComponent {

  public product: IProduct = new Product();
  public productModels: Array<IProductModel> = [];
  public selectedProductModel: IProductModel = {} as IProductModel;
  public sizeUnits = SizeUnits;
  public weightUnits = WeightUnits;

  private unsubAll: Subject<any>;

  constructor(
    private readonly productService: ProductService,
    private readonly uiService: UiService
  ) {
    this.unsubAll = new Subject();
  }

  ngOnInit() {
    this.productService.getAllProductModels()
      .pipe(
        takeUntil(this.unsubAll),
        map(models => this.productModels = models)
      ).subscribe();
  }

  public productModelChangeHandler(id: number): void {
    this.product.productModelId = id;
    this.selectedProductModel = this.productModels[this.productModels.map(x => x.productModelId).indexOf(id)]
  }

  public save(): void {
    const validationResult = this.productService.validate(this.product);

    if(!validationResult.isValid) {
      this.uiService.showToast(ToastType.error, validationResult.error);
      return;
    }

    this.productService.addProduct(this.product)
      .pipe(
        takeUntil(this.unsubAll),
        catchError(
          (err : HttpErrorResponse ): Observable<any> => {
            this.uiService.showToast(ToastType.error, err.error);
            console.log(err)
            return of({hasError: true});
        }),
        map(result => {
          if(!result.hasError) {
            this.uiService.showToast(ToastType.info, "Successully created new product")
          }
        })
        ).subscribe();
  }

  ngOnDestroy(): void {
    this.unsubAll.unsubscribe();
  }

}
