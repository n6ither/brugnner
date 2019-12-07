import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
    HttpHeaders
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { mergeMap, catchError } from 'rxjs/operators';
import { AuthService } from '../auth/auth.service';

@Injectable({
    providedIn: 'root'
})
export class JWTAuthInterceptor implements HttpInterceptor {

    constructor(private auth: AuthService) {

    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        if (!this.auth.loggedIn) {
            return next.handle(req);
        }

        return this.auth.getTokenSilently$().pipe(
            mergeMap(token => {

                const tokenReq = req.clone({
                    setHeaders: { Authorization: `Bearer ${token}`, Content: 'application/json' }
                });

                return next.handle(tokenReq);
            }),
            catchError(err => throwError(err))
        );
    }
}