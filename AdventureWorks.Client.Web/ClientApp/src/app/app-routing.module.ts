import { NgModule, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ProductsComponent } from './pages/products/products.component';
import { ProductOverviewComponent } from './pages/product-overview/product-overview.component';
import { EditProductComponent } from './pages/edit-product/edit-product.component';
import { AddProductComponent } from './pages/add-product/add-product.component';

const routes: Routes = [
  { path: "", component: HomeComponent},
  { path: "products", component: ProductsComponent},
  { path: "product-view", component: ProductOverviewComponent},
  { path: "products/add", component: AddProductComponent},
  { path: "products/edit", component: EditProductComponent}
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
