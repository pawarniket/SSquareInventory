import { Injectable } from '@angular/core';
import { MasterService } from '../master/master.service';
import { environment } from '../../../../environments/environment';
import { APIConstant } from '../../constant/APIConstant';
import { SidebarComponent } from '../../../pages/sidebar/sidebar.component';
import { BehaviorSubject } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class SalesdetailsService {
// selectedproducts:any[]=[];
private selectedProductsSubject = new BehaviorSubject<any[]>([]);
selectedProducts$ = this.selectedProductsSubject.asObservable();

constructor() {}

addProduct(product: any) {
  const updatedList = [...this.selectedProductsSubject.value, product];
  this.selectedProductsSubject.next(updatedList);
  console.log("Updated selected products:", updatedList);
}
}
