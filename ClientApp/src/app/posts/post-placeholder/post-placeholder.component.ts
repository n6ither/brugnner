import { Component, HostBinding } from '@angular/core';

@Component({
    selector: 'post-placeholder',
    templateUrl: 'post-placeholder.component.html',
    styleUrls: ['post-placeholder.component.scss'],
})
export class PostPlaceholderComponent {

    @HostBinding('attr.aria-label')
    label = 'Loading';
}
