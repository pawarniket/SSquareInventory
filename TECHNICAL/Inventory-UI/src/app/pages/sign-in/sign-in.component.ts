import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../core/service/user/user.service';
declare function Popupdisplay(message: any): any;

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.css'
})
export class SignINComponent {
  passwordVisible = false;
  SignInForm!: FormGroup;
  isPassEmpty = false
  isEmailEmpty  = false


  constructor(private fb: FormBuilder, 
     public router: Router,
     private user: UserService
  ) {
    //

  }  
  ngOnInit(): void {

  this.SignInForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required]
  });
}
  togglePasswordVisibility(): void {
    this.passwordVisible = !this.passwordVisible; 
  }
  get emailControl() {
    return this.SignInForm.get('email');
  }

  get passwordControl() {
    return this.SignInForm.get('password');
  }


  SignIn(){
    if(!this.SignInForm.valid){
      this.SignInForm.markAllAsTouched();

      return

  }

  const formvalue = this.SignInForm.value;

  const val = {
    Email: formvalue.email,
    Password: formvalue.password,

  }
  this.user.LoginUser(val).subscribe(
    response => {
console.log("response",response);

if(response.status_code == 100){
      this.router.navigate(['/StockManagement/Product']); // Default fallback route
}
else{
  Popupdisplay("User Not Found");

}
    });
    

}
}
