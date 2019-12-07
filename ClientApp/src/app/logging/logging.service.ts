import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable()
export class LoggingService {

    log(p1: any, p2?: any): void {
        if (!environment.production) {

            if (p2 !== undefined)
                console.log(p1, p2)
            else
                console.log(p1)
        }
    }
}
