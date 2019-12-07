import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {

    constructor(private authService: AuthService, private router: Router) {

    }

    ngOnInit() {

        if (!this.authService.loggedIn) {
            this.authService.login();
        } else {
            this.router.navigateByUrl('/home');
        }
    }
}