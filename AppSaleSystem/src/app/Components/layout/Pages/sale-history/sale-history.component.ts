import { Component, OnInit,AfterViewInit,ViewChild } from '@angular/core';

import { FormBuilder,FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';

import {MAT_DATE_FORMATS} from '@angular/material/core'
import * as moment from 'moment';

import { ModalDetailSaleComponent } from '../../Modals/modal-detail-sale/modal-detail-sale.component';

import { Sale } from 'src/app/Interfaces/sale';
import { SaleService } from 'src/app/Services/sale.service';
import { UtilityService } from 'src/app/Reusable/utility.service';

export const MY_DATA_FORMAT={
  parse:{
    dateInput:'DD/MM/YYYY'
  },
  display:{
    dateInput:'DD/MM/YYYY',
    monthYearLabel:"MMMM YYYY"
  }
}

@Component({
  selector: 'app-sale-history',
  templateUrl: './sale-history.component.html',
  styleUrls: ['./sale-history.component.css'],
  providers:[
    {provide:MAT_DATE_FORMATS,useValue:MY_DATA_FORMAT}
  ]
})
export class SaleHistoryComponent implements OnInit, AfterViewInit {

  searchForm:FormGroup;
  searchOption:any[]=[
    {value:"date",description:"By Date"},
    {value:"number",description:"Sale number"},
  ]
  columnsTable:string[]=["createdOn","numberDoc","paymentType","total","actions"]
  dataInit:Sale[]=[]
  dataListOfSale=new MatTableDataSource(this.dataInit);
  @ViewChild(MatPaginator) tablePagination!:MatPaginator;
  
  constructor(
    private fb:FormBuilder,
    private dialog:MatDialog,
    private _saleService:SaleService,
    private _utilityService:UtilityService
  ) { 
    this.searchForm=this.fb.group({
      searchBy:['date'],
      number:[''],
      startDate:[''],
      endDate:['']
    })

    this.searchForm.get("searchBy")?.valueChanges.subscribe(value=>{
      this.searchForm.patchValue({
        number:"",
        startDate:"",
        endDate:""
      })
    })


  }

  ngOnInit(): void {
  }
  ngAfterViewInit(): void {
      this.dataListOfSale.paginator = this.tablePagination;
  }

  addFilterTable(event:Event){
    const filterValue=(event.target as HTMLInputElement).value;
    this.dataListOfSale.filter=filterValue.trim().toLocaleLowerCase();
  }

  searchSales(){
    let _startDate:string="";
    let _endDate:string="";

    if(this.searchForm.value.searchBy==="date"){
      _startDate=moment(this.searchForm.value.startDate).format('DD/MM/YYYY');
      _endDate=moment(this.searchForm.value.endDate).format('DD/MM/YYYY');

      if(_startDate==="invalid date" || _endDate==="invalid date" ){
        this._utilityService.showAlert("You must enter both dates","Oops!")
        return;
      }
    }

    this._saleService.history(
      this.searchForm.value.searchBy,
      this.searchForm.value.number,
      _startDate,
      _endDate
    ).subscribe({
      next:(data)=>{
        if(data.status)
          this.dataListOfSale=data.value;
        else
          this._utilityService.showAlert("Data not found","Oops!")
      },
      error:(e)=>{}
    })

  }

  viewSaleDetail(_sale:Sale){
    this.dialog.open(ModalDetailSaleComponent,{
      data:_sale,
      disableClose:true,
      width:'700px'
    })
  }

}
