import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductService } from '../../../core/service/product/product.service';
import { ProductcategoryService } from '../../../core/service/productcategory/productcategory.service';
import { SalesdetailsService } from '../../../core/service/saledetails/salesdetails.service';


@Component({
  selector: 'app-sale-details',
  templateUrl: './sale-details.component.html',
  styleUrl: './sale-details.component.css'
})
export class SaleDetailsComponent {
  productform!: FormGroup;
  Productlist: any;
  ProductCategoryList:any;
  selectedProduct:any[]=[];
  constructor(private products: ProductService,
   private ProductcategoryService: ProductcategoryService,
    private formBuilder: FormBuilder,private saleservice:SalesdetailsService) {

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
          // this.closePopup("addProductModal");
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
          // this.closePopup("addProductModal");
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
  selectProduct(product: any) {
    let index = this.selectedProduct.findIndex(p => p.ProductID === product.ProductID);

    if (index === -1) {
      this.selectedProduct.push(product); 
      this.saleservice.addProduct(product);
    }else{
      alert("product is already selected")
    }
  }
  removeProduct(productId: number) {
    this.selectedProduct = this.selectedProduct.filter(p => p.ProductID !== productId);
    console.log("Updated Selected Products:", this.selectedProduct);
  }
  increaseQuantity(product: any) {
    product.StockQuantity++;
  }

  // Function to decrease quantity
  decreaseQuantity(product: any) {
    if (product.StockQuantity > 1) {
      product.StockQuantity--;
    }
  }
  saveDetails(){
    if(this.selectedProduct.length>0){
      console.log("this.selectProduct",this.selectedProduct)
    }else{
      alert("please select atleast one product to proceed")
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


  // closePopup(modalId: string) {
  //   const modal = document.getElementById(modalId);
  //   if (modal) {
  //     modal.style.display = "none"; // Or use Bootstrap modal hide: $('#modalId').modal('hide');
  //   }
  // }
  Resetform(){
    this.productform.reset();

  }
}
