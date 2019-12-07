import { NgModule } from '@angular/core';
import { NbCardModule, NbSpinnerModule } from '@nebular/theme';

import { AuthGuard } from './auth.guard';
import { CallbackComponent } from './callback/callback.component';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
    { path: '', component: LoginComponent },
    { path: 'login', component: LoginComponent },
    { path: 'callback', component: CallbackComponent },
    { path: 'logout', component: LogoutComponent }
];

@NgModule({
    declarations: [
        LoginComponent,
        CallbackComponent,
        LogoutComponent
    ],
    imports: [
        RouterModule.forChild(routes),
        NbCardModule,
        NbSpinnerModule
    ],
    providers: [
        AuthGuard
    ],
    exports: [
        RouterModule
    ]
})
export class AuthModule {

}
