import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';
import { Menu } from 'src/app/Interfaces/menu';

import { MenuService } from 'src/app/Services/menu.service';
import { UtilityService } from 'src/app/Reusable/utility.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {

  listOfMenu:Menu[]=[];
  userEmail:string="";
  userRol:string="";


  constructor(
    private router:Router,
    private _menuService:MenuService,
    private _utilityService:UtilityService
  ) { }

  ngOnInit(): void {

    const user=this._utilityService.getUserSession();

    if(user!=null){

      this.userEmail=user.email;
      this.userRol=user.rolDescription;

      this._menuService.list(user.idUsuario).subscribe({
        next:(data)=>{
          if(data.status) this.listOfMenu=data.value;
        },error:(e)=>{}
      })
    }
  }

  logOut(){
    this._utilityService.deleteUserSession();
    this.router.navigate(['login'])
  }
}
