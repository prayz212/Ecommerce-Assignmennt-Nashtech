import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NAVIGATE_URL } from '../../constants/navigate-url';
import { AuthService } from '../authentication/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  userInfo = {
    fullname: "Chi Vi",
    employeeId: "SD4882"
  };

  listUrl = NAVIGATE_URL;

  constructor(private authService: AuthService, private router: Router) { }


  onSignOut(): void {
    this.authService.currentValue = undefined;
    this.router.navigate([`${NAVIGATE_URL.AUTHENTICATION}/${NAVIGATE_URL.SIGN_IN}`]);
  }
}
