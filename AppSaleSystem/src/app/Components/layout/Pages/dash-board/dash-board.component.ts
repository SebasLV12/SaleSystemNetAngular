import { Component, OnInit } from '@angular/core';

import { Chart,registerables } from 'chart.js';
import { DashboardService } from 'src/app/Services/dashboard.service';
Chart.register(...registerables);


@Component({
  selector: 'app-dash-board',
  templateUrl: './dash-board.component.html',
  styleUrls: ['./dash-board.component.css']
})
export class DashBoardComponent implements OnInit {

  totalIncome:string="0";
  totalSale:string="0";
  totalProducts:string="0";

  constructor(
    private _dashboardService:DashboardService
  ) { }

  showChart(chartLabel:any[],chartData:any[]){
    const barsChart=new Chart('barsChart',{
      type:'bar',
      data:{
        labels:chartLabel,
        datasets:[{
          label:'# of Sale',
          data:chartData,
          backgroundColor:[
            'rgba(54,162,235,0.2)'
          ],
          borderColor:[
            'rgba(54,162,235,1'
          ],
          borderWidth:1
        }]
      },
      options:{
        maintainAspectRatio:false,
        responsive:true,
        scales:{
          y:{
            beginAtZero:true
          }
        }
      }
    });
  }

  ngOnInit(): void {
    this._dashboardService.resume().subscribe({
      next:(data)=>{
        if(data.status){
          this.totalIncome=data.value.totalIncome;
          this.totalSale=data.value.totalSale;
          this.totalProducts=data.value.totalProducts

          const arrayData:any[]=data.value.saleLastWeek;
          
          const labelTemp=arrayData.map((value)=>value.date);
          const dataTemp=arrayData.map((value)=>value.total);

          this.showChart(labelTemp,dataTemp)

        }
      },
      error:(e)=>{}
    })
  }

}
