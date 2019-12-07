import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ErrorComponent } from './error.component';
import { NbCardModule, NbButtonModule } from '@nebular/theme';

const routes: Routes = [
    { path: '', component: ErrorComponent },
    { path: '0', component: ErrorComponent },
    { path: '401', component: ErrorComponent },
    { path: '404', component: ErrorComponent },
    { path: '500', component: ErrorComponent },
]

@NgModule({
    imports: [
        RouterModule.forChild(routes),
        NbCardModule,
        NbButtonModule
    ],
    exports: [
        RouterModule
    ],
    declarations: [
        ErrorComponent
    ],
})
export class ErrorModule {

}