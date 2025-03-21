import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductService } from '../../../core/service/product/product.service';
import { ProductcategoryService } from '../../../core/service/productcategory/productcategory.service';

@Component({
  selector: 'app-addcategory',
  templateUrl: './addcategory.component.html',
  styleUrl: './addcategory.component.css'
})
export class AddcategoryComponent {
  Productcategory!:FormGroup;
  ProductCategoryList:any=[];
  constructor(private ProductcategoryService: ProductcategoryService,
    private formBuilder: FormBuilder) {

  }

    ngOnInit(): void {
      this.Productcategory = this.formBuilder.group({
        CategoryID: [null],
        CategoryName: ['', Validators.required],
        Description: [''],
       
  
      });
      this.getProductcategory();
    }
  Resetform(){
    this.Productcategory.reset();

  }
  category(){
    if (!this.Productcategory.valid) {
      this.Productcategory.markAllAsTouched();

      return
    }
    const formvalue = this.Productcategory.value;

    if (formvalue.CategoryID) {
      console.log("If ai gaya");
      const val = {
        CategoryID: formvalue.CategoryID,
        CategoryName: formvalue.ProductName,
        Description: formvalue.Description,
       

      }
      this.ProductcategoryService.UpdateProductcategory(val).subscribe(
        response => {
          console.log("response", response);
          // this.closePopup("addProductModal");
          this.Productcategory.reset();
        });

    }
    else {

      console.log("Product is created");
      const val = {
        CategoryName: formvalue.CategoryName,
        Description: formvalue.Description,
     

      }
      console.log("val", val);

      this.ProductcategoryService.AddProductcategory(val).subscribe(
        response => {
          console.log("response", response);
          // this.closePopup("addProductModal");
          this.Productcategory.reset();
        });
    }
  }



  
  getProductcategory() {
    const val = {
    }
    this.ProductcategoryService.getProductcategory(val).subscribe(
      response => {
        console.log("response", response);
        this.ProductCategoryList = JSON.parse(response['message']);
        console.log("hii", this.ProductCategoryList);
        this.filterBookingData = this.ProductCategoryList;

      });
  }

  
  editProduct(productCategory: any) {
    this.Productcategory.patchValue({
      CategoryID: productCategory.CategoryID,
      CategoryName: productCategory.CategoryName,
      Description: productCategory.Description,
   
    });


    console.log("Editing product:", productCategory);
  }


  currentPage = 1;
  itemsPerPage = 5;

  // data = Array.from({ length: 50 }, (_, i) => ({
  //   id: i + 1,
  //   name: `User ${i + 1}`,
  //   age: 20 + (i % 10)
  // }));


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
      this.filterBookingData = [...this.ProductCategoryList]; // Correctly copying data
    } else {
      this.filterBookingData = this.ProductCategoryList.filter((category: any) =>
        category.CategoryID.toString().includes(this.searchText) ||
        category.CategoryName.toLowerCase().includes(this.searchText.toLowerCase()) ||
        (category.Description && category.Description.toLowerCase().includes(this.searchText.toLowerCase())) // Handle null/undefined
      );
    }
  }
  
}
