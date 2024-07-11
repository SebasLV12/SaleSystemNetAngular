import { Component, OnInit,AfterViewInit,ViewChild } from '@angular/core';

import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';

import { ProductModalComponent } from '../../Modals/product-modal/product-modal.component';
import { Product } from 'src/app/Interfaces/product';
import { ProductService } from 'src/app/Services/product.service';
import { UtilityService } from 'src/app/Reusable/utility.service';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit,AfterViewInit {

  
  columnsTable: string[]=['name','category','stock','price','state','actions']
  dataInit: Product[]=[]
  dataListOfProduct=new MatTableDataSource(this.dataInit);
  @ViewChild(MatPaginator) tablePagination!: MatPaginator;

  constructor(
    private dialog:MatDialog,
    private _productService:ProductService,
    private _utilityService:UtilityService,
  ) { }

  getProduct(){
    this._productService.list().subscribe({
      next:(data)=>{
        if(data.status) 
          this.dataListOfProduct.data=data.value;
        else
          this._utilityService.showAlert("No data found","Oops!");
      },
      error:(e)=>{}
    })
    }

  ngOnInit(): void {
    this.getProduct();
  }
  ngAfterViewInit(): void {
    this.dataListOfProduct.paginator = this.tablePagination;
  }
  addFilterTable(event:Event){
    const filterValue=(event.target as HTMLInputElement).value;
    this.dataListOfProduct.filter=filterValue.trim().toLocaleLowerCase();
  }

  newProduct(){
    this.dialog.open(ProductModalComponent,{
      disableClose:true
    }).afterClosed().subscribe(response=>{
      if(response===true) this.getProduct();
    })
  }
  updateProduct(product:Product){
    this.dialog.open(ProductModalComponent,{
      disableClose:true,
      data:product
    }).afterClosed().subscribe(response=>{
      if(response===true) this.getProduct();
    })
  }

  deleteProduct(product:Product){
    Swal.fire({
      title:"Do you want to delete the product?",
      text:product.name,
      icon:"warning",
      confirmButtonColor:'#3085d6',
      confirmButtonText:"Yes, delete",
      showCancelButton:true,
      cancelButtonColor:'#d33',
      cancelButtonText:"No, back"
    }).then((response)=>{
      if(response.isConfirmed){
        this._productService.delete(product.idProduct).subscribe({
          next:(data)=>{
            if(data.status){
              this._utilityService.showAlert("The product was deleted","Done!")
            }else{
              this._utilityService.showAlert("The product cannot be updated","Done!")
            }
          },
          error:(e)=>{}
        })
      }
    })
  }
  
}
