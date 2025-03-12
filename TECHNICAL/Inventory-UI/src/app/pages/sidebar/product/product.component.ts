import { Component, } from '@angular/core';
import { ProductService } from '../../../core/service/product/product.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
declare function closePopup(id: any): any;

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent {
  productform!: FormGroup;
  Productlist: any;
  ProductCategoryList:any;
  constructor(private products: ProductService,
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
          this.closePopup("addProductModal");
          this.productform.reset();
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
          this.closePopup("addProductModal");
          this.productform.reset();
        });
    }
  }


  getProduct() {
    const val = {
    }
    this.products.getProduct(val).subscribe(
      response => {
        console.log("response", response);
        this.Productlist = JSON.parse(response['message']);
        console.log("hii", this.Productlist);

      });
  }

  getProductcategory() {
    const val = {
    }
    this.products.getProductcategory(val).subscribe(
      response => {
        console.log("response", response);
        this.ProductCategoryList = JSON.parse(response['message']);
        console.log("hii", this.Productlist);

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


  closePopup(modalId: string) {
    const modal = document.getElementById(modalId);
    if (modal) {
      modal.style.display = "none"; // Or use Bootstrap modal hide: $('#modalId').modal('hide');
    }
  }
  Resetform(){
    this.productform.reset();

  }
}