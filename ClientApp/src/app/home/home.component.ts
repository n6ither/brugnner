import { NbThemeService } from '@nebular/theme';
import { AuthService } from './../auth/auth.service';
import { Component } from '@angular/core';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html'
})
export class HomeComponent {

    constructor(public authService: AuthService, private themeService: NbThemeService) {

    }

    toggleDarkTheme(): void {
        if (this.themeService.currentTheme == 'default') {
            this.themeService.changeTheme('dark');
        }
        else {
            this.themeService.changeTheme('default');
        }
    }
}
