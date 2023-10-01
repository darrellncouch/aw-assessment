import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { IProductInformation } from 'src/app/core/IProductInformation';
import { ProductService } from 'src/app/service/product.service';

@Component({
  selector: 'edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.scss']
})
export class EditProductComponent {

  public product: IProductInformation = {} as IProductInformation;
  public isEdit: boolean = false;
  public id: number = 0;
  private unsubAll: Subject<any>;


  constructor(
    private readonly activatedRouter: ActivatedRoute,
    private readonly productService: ProductService
  )
  {
    this.unsubAll = new Subject();
  }

  ngOnInit(): void {
    this.productService._targetProduct
      .pipe(takeUntil(this.unsubAll))
      .subscribe(x => this.product = x);

    this.activatedRouter.queryParams
      .subscribe(param => {
        if(param.id == undefined)
        {
          this.isEdit = true;
          return;
        }
        this.id = parseInt(param.id);
      })

    if(this.id != Number.NaN)
    {
      this.productService.getProductById(this.id)
      .pipe(takeUntil(this.unsubAll))
      .subscribe(p => {
        this.productService.updateTarget(p);
      })
    }else {
      this.productService.updateTarget({} as IProductInformation);
    }




  }
}
