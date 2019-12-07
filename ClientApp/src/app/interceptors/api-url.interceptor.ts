import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable()
export class ApiUrlInterceptor implements HttpInterceptor {

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        req = req.clone({ url: this.prepareUrl(req.url) });

        return next.handle(req);
    }

    private isAbsoluteUrl(url: string): boolean {
        const absolutePattern = /^https?:\/\//i;

        return absolutePattern.test(url);
    }

    private prepareUrl(url: string): string {
        url = this.isAbsoluteUrl(url) ? url : environment.apiUrl + '/' + url;

        return url.replace(/([^:]\/)\/+/g, '$1');
    }
}