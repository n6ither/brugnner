import { APIResponse } from './../models/api/api-response.model';
import { Router } from '@angular/router';
import { NbToastrService, NbGlobalPhysicalPosition, NbToastrConfig } from '@nebular/theme';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

    private readonly toastrConfig: Partial<NbToastrConfig> = {
        position: NbGlobalPhysicalPosition.BOTTOM_RIGHT,
        duration: 10000,
        preventDuplicates: true,
        destroyByClick: true
    };

    constructor(private toastrService: NbToastrService, private router: Router) {

    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request)
            .pipe(
                retry(1),
                catchError((response: HttpErrorResponse) => {

                    let errorMessage = '';

                    if (response.error instanceof ErrorEvent) {

                        // client-side error
                        errorMessage = `Error: ${response.error.message}`;
                    } else {

                        // server-side error
                        if (response.status === 0) {
                            errorMessage = 'Connection refused (0)'
                            this.router.navigate(['/error/0']);
                        } else {

                            const apiResponse = response.error as APIResponse<any>;

                            if (apiResponse.statusCode === 500 || apiResponse.statusCode === 401 || apiResponse.statusCode === 404) {
                                this.router.navigate(['/error/' + apiResponse.statusCode]);
                            } else {
                                errorMessage = `${apiResponse.result.error.message} (${apiResponse.statusCode})`;

                                if (apiResponse.result.error.validationErrors) {
                                    for (let i = 0; i < apiResponse.result.error.validationErrors.length; i++) {
                                        const element = apiResponse.result.error.validationErrors[i];
                                        errorMessage += "\n" + element.field + ": " + element.message;
                                    }
                                }
                            }
                        }
                    }

                    this.toastrService.danger(errorMessage, 'Brugnner', this.toastrConfig);

                    return throwError(errorMessage);
                })
            )
    }
}