import { Component, OnInit,Inject } from '@angular/core';

import { FormBuilder,FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

import { Category } from 'src/app/Interfaces/category';
import { Product } from 'src/app/Interfaces/product';
import { CategoryService } from 'src/app/Services/category.service';
import { ProductService } from 'src/app/Services/product.service';
import { UtilityService } from 'src/app/Reusable/utility.service';


@Component({
  selector: 'app-product-modal',
  templateUrl: './product-modal.component.html',
  styleUrls: ['./product-modal.component.css']
})
export class ProductModalComponent implements OnInit {

  productForm:FormGroup;
  actionTitle:string="Add";
  actionButton:string="Save";
  listOfCategories:Category[]=[];


  constructor(
    private actualModal: MatDialogRef<ProductModalComponent>,
    @Inject(MAT_DIALOG_DATA) public productData: Product,
    private fb:FormBuilder,
    private _categoryService:CategoryService,
    private _productService:ProductService,
    private _utilityService:UtilityService
  ) { 

    this.productForm=this.fb.group({
      name:['',Validators.required],
      idCategory:['',Validators.required],
      stock:['',Validators.required],
      price:['',Validators.required],
      isActive:[1,Validators.required],
    })


    if(this.productData!=null){
      this.actionTitle="Edit";
      this.actionButton="Update"
    }

    this._categoryService.list().subscribe({
      next:(data)=>{
        if(data.status) this.listOfCategories=data.value
      },
      error:(e)=>{}
    })


  }

  ngOnInit(): void {
    if(this.productData!=null){
      this.productForm.patchValue({
        name:this.productData.name,
        idCategory:this.productData.idCategory,
        stock:this.productData.stock,
        price:this.productData.price,
        isActive:this.productData.isActive.toString()
      })
    }
  }
  saveEdit_Product(){
    const _product:Product={
      idProduct:this.productData==null?0:this.productData.idProduct,
      name:this.productForm.value.name,
      idCategory:this.productForm.value.idCategory,
      categoryDescription:"",
      price:this.productForm.value.price,
      stock:this.productForm.value.stock,
      isActive:parseInt(this.productForm.value.isActive)
    }
    
    if(this.productData==null){
      this._productService.create(_product).subscribe({
        next:(data)=>{
          if(data.status){
            this._utilityService.showAlert("The product was register","Sucess");
            this.actualModal.close("true")
          }else
            this._utilityService.showAlert("Can not register the product","Error")
        },
          error:(e)=>{}
      })
    }else{
        this._productService.update(_product).subscribe({
          next:(data)=>{
            if(data.status){
              this._utilityService.showAlert("The product was updated","Sucess");
              this.actualModal.close("true")
            }else
              this._utilityService.showAlert("Can not update the product","Error")
          },
            error:(e)=>{}
        })
    }


  }

}

