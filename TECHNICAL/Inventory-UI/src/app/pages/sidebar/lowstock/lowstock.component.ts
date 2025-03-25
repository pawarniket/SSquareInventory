import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductcategoryService } from '../../../core/service/productcategory/productcategory.service';
import { ProductService } from '../../../core/service/product/product.service';
declare function Popupdisplay(message: any): any;

@Component({
  selector: 'app-lowstock',
  templateUrl: './lowstock.component.html',
  styleUrl: './lowstock.component.css'
})
export class LowstockComponent {
  StockList:any=[];
    productform!: FormGroup;
    ProductCategoryList:any;
    Productlist: any;

  constructor(private products : ProductService,
    private ProductcategoryService: ProductcategoryService,
     private formBuilder: FormBuilder) {

  }
  ngOnInit(): void {
        this.productform = this.formBuilder.group({
          ProductID: [null],
          ProductName: ['', Validators.required],
          ProductDescription: ['', Validators.required],
          ProductCategory: ['', Validators.required],
          Price: ['', Validators.required],
          Quantity: ['', Validators.required],
          RackNumber: ['', Validators.required],
          ISActive: true
    
        });
        this.getProduct();
this.getProductcategory();
  }

  // // getProductcategory() {
  // //   const val = {
  // //   }
  // //   this.ProductcategoryService.getProductcategory(val).subscribe(
  // //     response => {
  // //       console.log("response", response);
  // //       this.StockList = JSON.parse(response['message']);
  // //       console.log("hii", this.StockList);
  // //       this.filterBookingData = this.StockList;
  // //       if (this.filterBookingData[0]?.Message === 'Data not found') {
  // //         this.filterBookingData = [];
  // //       }
  // //     });
  // // }

  updateproduct(){
    if (!this.productform.valid) {
      this.productform.markAllAsTouched();

      return
    }
    const formvalue = this.productform.value;

    if (formvalue.ProductID) {
      console.log("If ai gaya");
      const val = {
        ProductID: formvalue.ProductID,
        ProductName: formvalue.ProductName,
        Description: formvalue.ProductDescription,
        CategoryID: formvalue.ProductCategory,
        Price: formvalue.Price,
        StockQuantity: formvalue.Quantity,
        RackNumber: formvalue.RackNumber,
        ISActive: formvalue.ISActive

      }
      this.products.UpdateProduct(val).subscribe(
        response => {
          console.log("response", response);
          this.closePopup();
          this.productform.reset();
          this.getProduct();

        });
      }
  }
  closePopup() {
    var modal = document.getElementById("closebtn") as HTMLElement
    modal.click();
  }
  Resetform(){
    this.productform.reset();

  }
  getProduct() {
    const val = {
    }
    this.products.getProduct(val).subscribe(
      response => {
        console.log("response", response);
        this.StockList = JSON.parse(response['message']);
console.log("this.StockList",this.StockList.StockQuantity>5);
const StockQuantity = this.StockList.filter((item: any) => item.StockQuantity <=5 && item.StockQuantity !== 0);
console.log("StockQuantity",StockQuantity);

        this.filterBookingData = StockQuantity;
        if (this.filterBookingData[0]?.Message === 'Data not found') {
          this.filterBookingData = [];
        }
      });
  }

  getProductcategory() {
    const val = {
    }
    this.ProductcategoryService.getProductcategory(val).subscribe(
      response => {
        console.log("response", response);
        this.ProductCategoryList = JSON.parse(response['message']);
        console.log("hii", this.Productlist);
        if (this.ProductCategoryList[0]?.Message === 'Data not found') {
          this.ProductCategoryList = [];
        }
      });
  }

  outofstockData() {
    const val = {
    }
    this.products.getProduct(val).subscribe(
      response => {
        console.log("response", response);
        this.StockList = JSON.parse(response['message']);
console.log("this.StockList",this.StockList.StockQuantity>5);
const StockQuantity = this.StockList.filter((item: any) => item.StockQuantity == 0);
console.log("StockQuantity",StockQuantity);

        this.filterBookingData = StockQuantity;
        if (this.filterBookingData[0]?.Message === 'Data not found') {
          this.filterBookingData = [];
        }
      });
  }
  currentPage = 1;
  itemsPerPage = 5;



  get totalPages(): number {
    return Math.ceil(this.filterBookingData.length / this.itemsPerPage);
  }

  getPaginatedData() {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    return this.filterBookingData.slice(startIndex, startIndex + this.itemsPerPage);
  }
  nextPage() {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
    }
  }

  prevPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }

  searchText: any;
  filterBookingData: any = [];

  onSearch(event: any) {
    this.searchText = event.target.value;

    this.applySearch();
  }

  applySearch() {
    if (!this.searchText) {
      this.filterBookingData = [...this.StockList]; // Correctly copying data
    } else {
      this.filterBookingData = this.StockList.filter((category: any) =>
        category.Price.toString().includes(this.searchText) ||
      category.RackNumber.toString().includes(this.searchText) ||
      category.StockQuantity.toString().includes(this.searchText) ||

        category.ProductName.toLowerCase().includes(this.searchText.toLowerCase()) ||
        category.CategoryName.toLowerCase().includes(this.searchText.toLowerCase()) 

      );
    }
  }
  editProduct(product: any) {
    this.productform.patchValue({
      ProductID: product.ProductID,
      ProductName: product.ProductName,
      ProductDescription: product.Description,
      ProductCategory: product.CategoryID,
      Price: product.Price,
      Quantity: product.StockQuantity,
      ISActive: product.IsActive,
      RackNumber: product.RackNumber
    });


    console.log("Editing product:", product);
  }

  deleteproduct(product:any){
    console.log(product.ProductID,"product");
    const val  ={
      ProductID:product.ProductID
    }
    this.products.deleteProduct(val).subscribe(
      response => {
              Popupdisplay('Data Deleted Successfully');
              this.getProduct()
      });
      }

}
