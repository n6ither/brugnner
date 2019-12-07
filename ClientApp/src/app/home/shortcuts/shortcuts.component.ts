import { Component, OnInit } from '@angular/core';
import { NbThemeService, NbMediaBreakpointsService } from '@nebular/theme';
import { map, takeUntil } from 'rxjs/operators';

@Component({
    selector: 'app-shortcuts',
    templateUrl: 'shortcuts.component.html',
    styleUrls: ['./shortcuts.component.scss']
})
export class ShortcutsComponent implements OnInit {

    mobile: boolean = false;

    constructor(private themeService: NbThemeService,
        private breakpointService: NbMediaBreakpointsService) {

    }

    ngOnInit() {
        const { xl } = this.breakpointService.getBreakpointsMap();
        this.themeService.onMediaQueryChange()
            .pipe(map(([, currentBreakpoint]) => currentBreakpoint.width < xl))
            .subscribe((isLessThanXl: boolean) => this.mobile = isLessThanXl);
    }
}