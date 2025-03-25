import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
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
import { LowstockComponent } from './pages/sidebar/lowstock/lowstock.component';
import { SaleDetailsComponent } from './pages/sidebar/sale-details/sale-details.component';
import { loaderInterceptor } from './core/interceptors/loader.interceptor';
import { LoaderComponent } from './pages/loader/loader.component';

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
    LowstockComponent,
    LoaderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
ReactiveFormsModule,
  ],
  providers: [
    
    provideClientHydration(withEventReplay()),

    {
      provide: HTTP_INTERCEPTORS,
      useClass: loaderInterceptor,
      multi: true
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
