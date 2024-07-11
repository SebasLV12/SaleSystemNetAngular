import { Component, OnInit } from '@angular/core';

import { FormBuilder,FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { ProductService } from 'src/app/Services/product.service';
import { SaleService } from 'src/app/Services/sale.service';
import { UtilityService } from 'src/app/Reusable/utility.service';

import { Product } from 'src/app/Interfaces/product';
import { Sale } from 'src/app/Interfaces/sale';
import { DetailSale } from 'src/app/Interfaces/detail-sale';

import Swal from 'sweetalert2';
import { NumberSymbol } from '@angular/common';

@Component({
  selector: 'app-sale',
  templateUrl: './sale.component.html',
  styleUrls: ['./sale.component.css']
})
export class SaleComponent implements OnInit {

  listOfProducts:Product[]=[];
  listOfProductFilter:Product[]=[];

  listOfProductForSale:DetailSale[]=[];
  blockButtonRegister:boolean=false;

  selectedProduct!:Product;
  paymentTypeByDefault:string="Cash";
  TotalToPay:number=0;

  productSaleForm:FormGroup;
  columnsTable:string[]=['product','amount','price','total','actions']
  dataDetailSale=new MatTableDataSource(this.listOfProductForSale);

  returnProductsByFilter(filter:any):Product[]{
    const filterValue= typeof filter==="string"?filter.toLocaleLowerCase():filter.name.toLocaleLowerCase();

    return this.listOfProducts.filter(item=>item.name.toLocaleLowerCase().includes(filterValue))
  }

  constructor(
    private fb:FormBuilder,
    private _productService:ProductService,
    private _saleService:SaleService,
    private _utilityService:UtilityService
  ) { 
      this.productSaleForm=this.fb.group({
        product:['',Validators.required],
        amount:['',Validators.required],

      })
      this._productService.list().subscribe({
        next:(data)=>{
          if(data.status) {
            const list=data.value as Product[];
            this.listOfProducts=list.filter(p=>p.isActive==1 && p.stock>0)
          }
        },
        error:(e)=>{}
      })

      this.productSaleForm.get('product')?.valueChanges.subscribe(value=>{
        this.listOfProductFilter=this.returnProductsByFilter(value)
      })

  }

  ngOnInit(): void {
  }

  showProduct(product:Product): string{
    return product.name;
  }

  productForSale(event:any){
    this.selectedProduct=event.option.value;
  }
  
  addProductForSale(){
    const _amount:number=this.productSaleForm.value.amount;
    const _price:number=parseFloat(this.selectedProduct.price);
    const _total:number=_amount*_price;
    this.TotalToPay=this.TotalToPay+_total;

    this.listOfProductForSale.push({
      idProduct:this.selectedProduct.idProduct,
      productDescription:this.selectedProduct.name,
      amount:_amount,
      priceString:String(_price.toFixed(2)),
      totalString:String(_total.toFixed(2)),
    })

    this.dataDetailSale=new MatTableDataSource(this.listOfProductForSale);

    this.productSaleForm.patchValue({
      product:'',
      amount:''
    })
  }

  deleteProduct(detail:DetailSale){
    this.TotalToPay=this.TotalToPay-parseFloat(detail.totalString),
    this.listOfProductForSale=this.listOfProductForSale.filter(p=>p.idProduct!=detail.idProduct);
    
    this.dataDetailSale=new MatTableDataSource(this.listOfProductForSale);

  }

  registerSale(){
    if(this.listOfProductForSale.length>0){
      this.blockButtonRegister=true;
      const request: Sale={
        paymentType:this.paymentTypeByDefault,
        totalString:String(this.TotalToPay.toFixed(2)),
        detailSales:this.listOfProductForSale
      }
      
      this._saleService.register(request).subscribe({
        next:(response) =>{
          if(response.status){
            this.TotalToPay=0.00;
            this.listOfProductForSale=[];
            this.dataDetailSale=new MatTableDataSource(this.listOfProductForSale);

            Swal.fire({
              icon:'success',
              title:'Sale registered!',
              text:`Sale number: ${response.value.numberDoc}`
            })
          }else{
            this._utilityService.showAlert("Can not register the sale","Oops");
          }
        },
        complete:()=>{
          this.blockButtonRegister=false;
        },
        error:(e)=>{}
      })
    }
  }
}
