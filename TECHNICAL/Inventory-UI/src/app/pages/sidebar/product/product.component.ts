import { Component, } from '@angular/core';
import { ProductService } from '../../../core/service/product/product.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductcategoryService } from '../../../core/service/productcategory/productcategory.service';
// declare function closePopup(id: any): any;
declare function Popupdisplay(message: any): any;
  

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent {
  productform!: FormGroup;
  Productlist: any;
  ProductCategoryList:any;
  StockList:any=[];

  constructor(private products: ProductService,
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
    this.getProductcategory()
  }

  Product() {
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
    else {

      console.log("Product is created");
      const val = {
        ProductName: formvalue.ProductName,
        Description: formvalue.ProductDescription,
        CategoryID: formvalue.ProductCategory,
        Price: formvalue.Price,
        StockQuantity: formvalue.Quantity,
        RackNumber: formvalue.RackNumber,
        ISActive: formvalue.ISActive

      }
      console.log("val", val);

      this.products.AddProduct(val).subscribe(
        response => {
          console.log("response", response);
          this.closePopup();
          this.productform.reset();
          this.getProduct();
        });
    }
  }

  getProduct() {
    const val = {
    }
    this.products.getProduct(val).subscribe(
      response => {
        console.log("response", response);
        this.StockList = JSON.parse(response['message']);
console.log("this.StockList",this.StockList.StockQuantity>5);

        this.filterBookingData =this.StockList;
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


  closePopup() {
    var modal = document.getElementById("closebtn") as HTMLElement
    modal.click();
  }

  Resetform(){
    this.productform.reset();

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