import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration, withEventReplay } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SignINComponent } from './pages/sign-in/sign-in.component';
import { ProductComponent } from './pages/sidebar/product/product.component';
import { SidebarComponent } from './pages/sidebar/sidebar.component';
import { AddProductComponent } from './pages/sidebar/product/add-product/add-product.component';
import { AddcategoryComponent } from './pages/sidebar/addcategory/addcategory.component';
import { HeaderComponent } from './pages/Layout/header/header.component';
import { SaleDetailsComponent } from './pages/sidebar/sale-details/sale-details.component';

@NgModule({
  declarations: [
    AppComponent,
    SignINComponent,
    ProductComponent,
    SidebarComponent,
    AddProductComponent,
    AddcategoryComponent,
    HeaderComponent,
    SaleDetailsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
ReactiveFormsModule,
  ],
  providers: [
    provideClientHydration(withEventReplay())
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
