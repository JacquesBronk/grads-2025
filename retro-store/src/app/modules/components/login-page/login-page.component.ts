import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-page',
  imports: [FormsModule],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.scss'
})
export class LoginPageComponent {
  user = {
    username: '',
    password: '',
  };

  constructor(private router: Router) {}

  onSubmit() {
    // Simulate authentication
    if (this.user.username === 'user' && this.user.password === 'password') {
      localStorage.setItem('isAuthenticated', 'true');
      this.router.navigate(['/home']);
    } else {
      alert('Invalid credentials');
    }
  }
}
