import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Login } from '../login.model';

@Component({
  selector: 'login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {
  @Output() onSubmitEvent = new EventEmitter();

  login!: Login;

  constructor() { }

  ngOnInit(): void {
    this.login = new Login();
  }

  onSubmit(formData: NgForm): void {
    const emitData: any = {
      valid: formData.valid,
      data: formData.value,
    };

    this.onSubmitEvent.emit(emitData);
  }
}
