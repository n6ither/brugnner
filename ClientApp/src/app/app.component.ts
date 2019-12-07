import { LoggingService } from './logging/logging.service';
import { AuthService } from './auth/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app',
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {

  constructor(private authService: AuthService, private loggingService: LoggingService) {

  }

  ngOnInit(): void {
    this.authService.localAuthSetup();

    this.authService.userProfile$.subscribe(user => {
      this.loggingService.log("User", user);
    });
  }
}
