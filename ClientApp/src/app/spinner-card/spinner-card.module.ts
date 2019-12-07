import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { SpinnerCardComponent } from './spinner-card.component';
import { NbSpinnerModule, NbCardModule } from '@nebular/theme';

@NgModule({
    imports: [
        CommonModule,
        NbCardModule,
        NbSpinnerModule
    ],
    exports: [SpinnerCardComponent],
    declarations: [SpinnerCardComponent],
    providers: [],
})
export class SpinnerCardModule {

}
