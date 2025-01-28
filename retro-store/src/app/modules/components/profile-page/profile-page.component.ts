import { Component } from '@angular/core';

@Component({
  selector: 'app-profile-page',
  imports: [],
  templateUrl: './profile-page.component.html',
  styleUrl: './profile-page.component.scss'
})
export class ProfilePageComponent {
  user = {
    name: 'customeruser',
    email: 'ed63df81-52c2-4f70-8227-50eb1ce39b31',
    bio: 'A software developer passionate about clean code and innovative solutions.',
  };
}

