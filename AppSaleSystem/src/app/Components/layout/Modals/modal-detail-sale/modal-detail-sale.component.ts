import { Component, OnInit, Inject } from '@angular/core';

import { MAT_DIALOG_DATA} from '@angular/material/dialog';
import { Sale } from 'src/app/Interfaces/sale';
import { DetailSale } from 'src/app/Interfaces/detail-sale';

@Component({
  selector: 'app-modal-detail-sale',
  templateUrl: './modal-detail-sale.component.html',
  styleUrls: ['./modal-detail-sale.component.css']
})
export class ModalDetailSaleComponent implements OnInit {

  createOn:string="";
  numberDoc:string="";
  paymentType:string="";
  total:string="";
  detailSale:DetailSale[]=[];
  columnsTable:string[]=['product','amount','price','total']

  constructor(
    @Inject(MAT_DIALOG_DATA) public _sale: Sale,
  ) { 

    this.createOn=_sale.createdOn!;
    this.numberDoc=_sale.numberDoc!;
    this.paymentType=_sale.paymentType;
    this.total=_sale.totalString;
    this.detailSale=_sale.detailSales;

  }

  ngOnInit(): void {
  }

}
