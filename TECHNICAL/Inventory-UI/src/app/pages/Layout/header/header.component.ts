import { Component } from '@angular/core';
import { UserService } from '../../../core/service/user/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
constructor(private  user :UserService,private router :Router){

}
  SignOut() {
    try {
      this.user.isAuthenticated = false;
      this.user.currentUser = null;
      this.user.currentUserID = null;
      localStorage.removeItem('currentUser');
      this.router.navigate(['/login']);
    } catch (error) {
      console.error('Error during sign out:', error);
    }
  }
}
