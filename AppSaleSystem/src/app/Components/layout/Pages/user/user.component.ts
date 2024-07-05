import { Component, OnInit,AfterViewInit,ViewChild } from '@angular/core';

import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';

import { UserModalComponent } from '../../Modals/user-modal/user-modal.component';
import { User } from 'src/app/Interfaces/user';
import { UserService } from 'src/app/Services/user.service';
import { UtilityService } from 'src/app/Reusable/utility.service';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit, AfterViewInit{

  columnsTable: string[]=['fullName','email','rolDescription','state','actions']
  dataInit: User[]=[]
  dataListOfUser=new MatTableDataSource(this.dataInit);
  @ViewChild(MatPaginator) tablePagination!: MatPaginator;


  constructor(
    private dialog:MatDialog,
    private _userService:UserService,
    private _utilityService:UtilityService,

  ) { }

  getUser(){
    this._userService.list().subscribe({
      next:(data)=>{
        if(data.status) 
          this.dataListOfUser.data=data.value;
        else
          this._utilityService.showAlert("No data found","Oops!");
      },
      error:(e)=>{}
    })
    }

  ngOnInit(): void {
    this.getUser();
  }
  ngAfterViewInit(): void {
      this.dataListOfUser.paginator = this.tablePagination;
  }

  addFilterTable(event:Event){
    const filterValue=(event.target as HTMLInputElement).value;
    this.dataListOfUser.filter=filterValue.trim().toLocaleLowerCase();
  }
  
  newUser(){
    this.dialog.open(UserModalComponent,{
      disableClose:true
    }).afterClosed().subscribe(response=>{
      if(response===true) this.getUser();
    })
  }
  updateUser(user:User){
    console.log(user)
    this.dialog.open(UserModalComponent,{
      disableClose:true,
      data:user
    }).afterClosed().subscribe(response=>{
      if(response===true) this.getUser();
    })
  }
  deleteUser(user:User){
    Swal.fire({
      title:"Do you want to delete the user?",
      text:user.fullName,
      icon:"warning",
      confirmButtonColor:'#3085d6',
      confirmButtonText:"Yes, delete",
      showCancelButton:true,
      cancelButtonColor:'#d33',
      cancelButtonText:"No, back"
    }).then((response)=>{
      if(response.isConfirmed){
        this._userService.delete(user.idUsuario).subscribe({
          next:(data)=>{
            if(data.status){
              this._utilityService.showAlert("The user was deleted","Done!")
            }else{
              this._utilityService.showAlert("The user cannot be updated","Done!")
            }
          },
          error:(e)=>{}
        })
      }
    })
  }
}
