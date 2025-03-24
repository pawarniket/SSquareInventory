import { Component } from '@angular/core';
import { SaleDetailsComponent } from './sale-details/sale-details.component';
import { SalesdetailsService } from '../../core/service/saledetails/salesdetails.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  selectedproducts:any[]=[];
  isSidebarHidden = false;
constructor(private saleservice:SalesdetailsService){
  
}
  toggleSidebar() {
    this.isSidebarHidden = !this.isSidebarHidden;
  }

  ngOnInit() {
    this.saleservice.selectedProducts$.subscribe((products:any) => {
      this.selectedproducts = products;
      console.log("Updated Selected Products in Other Component:", this.selectedproducts);
    });
  }
}
