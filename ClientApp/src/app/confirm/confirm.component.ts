import { Component, Input } from '@angular/core';
import { NbDialogRef } from '@nebular/theme';

@Component({
    selector: 'confirm',
    templateUrl: './confirm.component.html',
    styleUrls: ['./confirm.component.scss']
})
export class ConfirmComponent {

    @Input() title: string;
    @Input() text: string;

    constructor(protected ref: NbDialogRef<ConfirmComponent>) { }

    yes() {
        this.ref.close(true);
    }

    no() {
        this.ref.close(false);
    }
}
