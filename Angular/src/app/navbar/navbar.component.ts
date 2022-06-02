import { Component } from '@angular/core';
import { NAVIGATE_URL } from '../../constants/navigate-url';

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

  constructor() { }


  onSignOut(): void {
    console.log("clicked");
  }
}
