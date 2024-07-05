import { Component, OnInit,Inject } from '@angular/core';

import { FormBuilder,FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { Rol } from 'src/app/Interfaces/rol';
import { User } from 'src/app/Interfaces/user';

import { RolService } from 'src/app/Services/rol.service';
import { UserService } from 'src/app/Services/user.service';
import { UtilityService } from 'src/app/Reusable/utility.service';


@Component({
  selector: 'app-user-modal',
  templateUrl: './user-modal.component.html',
  styleUrls: ['./user-modal.component.css']
})
export class UserModalComponent implements OnInit {

  userForm:FormGroup;
  hidePassword:boolean=true;
  actionTitle:string="Add";
  actionButton:string="Save";
  listOfRoles:Rol[]=[];

  constructor(
    private actualModal: MatDialogRef<UserModalComponent>,
    @Inject(MAT_DIALOG_DATA) public userData: User,
    private fb:FormBuilder,
    private _rolService:RolService,
    private _userService:UserService,
    private _utilityService:UtilityService
  ) { 
    this.userForm=this.fb.group({
      fullName:['',Validators.required],
      email:['',Validators.required],
      idRol:['',Validators.required],
      password:['',Validators.required],
      isActive:['1',Validators.required],
    });

    if(this.userData!=null){
      this.actionTitle="Edit";
      this.actionButton="Update"
    }
    this._rolService.list().subscribe({
      next:(data)=>{
        if(data.status) this.listOfRoles=data.value
      },
      error:(e)=>{}
    })

  }

  ngOnInit(): void {
    if(this.userData!=null){
      this.userForm.patchValue({
        fullName:this.userData.fullName,
        email:this.userData.email,
        idRol:this.userData.idRol,
        password:this.userData.password,
        isActive:this.userData.isActive.toString(),
      })
    }
  }

  saveEdit_User(){
    const _user:User={
      idUsuario:this.userData==null?0:this.userData.idUsuario,
      fullName:this.userForm.value.fullName,
      email:this.userForm.value.email,
      idRol:this.userForm.value.idRol,
      rolDescription:"",
      password:this.userForm.value.password,
      isActive:parseInt(this.userForm.value.isActive)
    }
    
    if(this.userData==null){
      this._userService.create(_user).subscribe({
        next:(data)=>{
          if(data.status){
            this._utilityService.showAlert("The user was created","Sucess");
            this.actualModal.close("true")
          }else
            this._utilityService.showAlert("Can not create the user","Error")
        },
          error:(e)=>{}
      })
    }else{
        this._userService.update(_user).subscribe({
          next:(data)=>{
            if(data.status){
              this._utilityService.showAlert("The user was updated","Sucess");
              this.actualModal.close("true")
            }else
              this._utilityService.showAlert("Can not update the user","Error")
          },
            error:(e)=>{}
        })
    }


  }

}
