import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/AccountService.service';
import { User } from './_models/user';
import { PresenceService } from './_services/presence.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Dating app';
  users: any;

  constructor(private accountService: AccountService, private presence: PresenceService) {}

  ngOnInit(): void {
  this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user: User = JSON.parse(userString);
   //this.accountService.setCurrentUser(user);
   if (user) {
    this.accountService.setCurrentUser(user);
    this.presence.createHubConnection(user);
  }
  }
}
