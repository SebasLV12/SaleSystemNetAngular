import { Component, OnInit,AfterViewInit,ViewChild } from '@angular/core';

import { FormBuilder,FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';

import {MAT_DATE_FORMATS} from '@angular/material/core'
import * as moment from 'moment';

import * as XLSX from "xlsx"

import { Report } from 'src/app/Interfaces/report';
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
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css'],
  providers:[
    {provide:MAT_DATE_FORMATS,useValue:MY_DATA_FORMAT}
  ]
})
export class ReportComponent implements OnInit {

  filterForm:FormGroup;
  listOfSaleReport:Report[]=[];
  columnsTable:string[]=['createdOn','numberDoc','paymentType','total','product','amount','price','productTotal'];
  dataSaleReport=new MatTableDataSource(this.listOfSaleReport);
  @ViewChild(MatPaginator) tablePagination!:MatPaginator;


  constructor(
    private fb:FormBuilder,
    private _saleService:SaleService,
    private _utilityService:UtilityService
  ) { 

    this.filterForm=this.fb.group({
      startDate:['',Validators.required],
      endDate:['',Validators.required]
    })
  }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.dataSaleReport.paginator = this.tablePagination;
  } 

  searchSales(){
    const _startDate=moment(this.filterForm.value.startDate).format('DD/MM/YYYY');
    const _endDate=moment(this.filterForm.value.endDate).format('DD/MM/YYYY');

    if(_startDate==="invalid date" || _endDate==="invalid date" ){
      this._utilityService.showAlert("You must enter both dates","Oops!")
      return;
    }
    
    this._saleService.report(
      _startDate,
      _endDate
    ).subscribe({
      next:(data)=>{
        if(data.status){
          this.listOfSaleReport=data.value;
          this.dataSaleReport.data=data.value;
        }else{
          this.listOfSaleReport=[];
          this.dataSaleReport.data=[];
          this._utilityService.showAlert("No data found","Oops!")
        }
      },
      error:(e)=>{}
    })

  }

  exportExcel(){
    const wb=XLSX.utils.book_new();
    const ws=XLSX.utils.json_to_sheet(this.listOfSaleReport);
    
    XLSX.utils.book_append_sheet(wb,ws,"Report");
    XLSX.writeFile(wb,"Sales Report.xlsx");
  }
}
