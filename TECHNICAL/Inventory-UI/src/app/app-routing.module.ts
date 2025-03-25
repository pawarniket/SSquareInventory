import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignINComponent } from './pages/sign-in/sign-in.component';
import { SidebarComponent } from './pages/sidebar/sidebar.component';
import { ProductComponent } from './pages/sidebar/product/product.component';
import { AddProductComponent } from './pages/sidebar/product/add-product/add-product.component';
import { AddcategoryComponent } from './pages/sidebar/addcategory/addcategory.component';
import { LowstockComponent } from './pages/sidebar/lowstock/lowstock.component';
import { SaleDetailsComponent } from './pages/sidebar/sale-details/sale-details.component';
import { AuthGuard } from './core/guards/auth.guard';


const routes: Routes = [
  // { path: '', component: SignINComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: SignINComponent },

  { path: 'StockManagement', component: SidebarComponent, 
    canActivate: [AuthGuard],

    children:[
      { path: 'Product', component: ProductComponent , canActivate: [AuthGuard],

        children:[
          { path: 'AddProduct', component: AddProductComponent, canActivate: [AuthGuard] }
        ]
       },
       {path: 'Category', component: AddcategoryComponent
        , canActivate: [AuthGuard]
       },
       {path: 'LowStock', component: LowstockComponent,
        canActivate: [AuthGuard]
       },
       {path: 'SaleDetails', component: SaleDetailsComponent,
        canActivate: [AuthGuard]
       }


    ]
   },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
