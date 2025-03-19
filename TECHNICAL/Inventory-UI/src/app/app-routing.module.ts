import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignINComponent } from './pages/sign-in/sign-in.component';
import { SidebarComponent } from './pages/sidebar/sidebar.component';
import { ProductComponent } from './pages/sidebar/product/product.component';
import { AddProductComponent } from './pages/sidebar/product/add-product/add-product.component';
import { AddcategoryComponent } from './pages/sidebar/addcategory/addcategory.component';


const routes: Routes = [
  { path: '', component: SignINComponent },
  { path: 'StockManagement', component: SidebarComponent,

    children:[
      { path: 'Product', component: ProductComponent,

        children:[
          { path: 'AddProduct', component: AddProductComponent }
        ]
       },
       {path: 'Category', component: AddcategoryComponent}


    ]
   },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
