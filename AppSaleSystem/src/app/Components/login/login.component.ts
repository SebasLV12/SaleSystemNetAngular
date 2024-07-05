import { Component, OnInit } from '@angular/core';

import {FormBuilder,FormGroup,Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {Login} from 'src/app/Interfaces/login';
import { UserService } from 'src/app/Services/user.service';
import { UtilityService } from 'src/app/Reusable/utility.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  formLogin:FormGroup;
  hidePassword:boolean=true;
  showLoading:boolean=false;

  constructor(
    private fb:FormBuilder,
    private router:Router,
    private _userService:UserService,
    private _utilityService:UtilityService) { 
  
    this.formLogin=this.fb.group({
      email:['',Validators.required],
      password:['',Validators.required]
    })
  }

  ngOnInit(): void {
  }

  Login(){
    this.showLoading=true;

    const request:Login={
      email:this.formLogin.value.email,
      password:this.formLogin.value.password
    }
    this._userService.login(request).subscribe({
      next: (data)=>{
        if(data.status){
          this._utilityService.saveUserSession(data.value);
          this.router.navigate(["pages"])
        }else{
          this._utilityService.showAlert("Does not exist match","Opps!")
        }
      },
      complete:()=>{
        this.showLoading=false
      },
      error:()=>{
        this._utilityService.showAlert("An error occurred","Opps!")
      }
    })
  }

}
