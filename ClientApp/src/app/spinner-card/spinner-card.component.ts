import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-spinner-card',
    templateUrl: 'spinner-card.component.html'
})

export class SpinnerCardComponent {

    @Input()
    condition: boolean;
}