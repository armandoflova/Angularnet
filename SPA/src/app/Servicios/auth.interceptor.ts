import {Injectable} from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/Operators';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    intercept( req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError(error => {
                if (error.status === 401) {
                    return throwError (error.statusText);
                }
                if ( error instanceof HttpErrorResponse){
                    const aplicationError = error.headers.get('Application-Error');
                    if (aplicationError) {
                        console.log(aplicationError);
                        return throwError(aplicationError);
                    }
                    const serverError = error.error;
                    let modalStateError = '';
                    if (serverError && typeof serverError === 'object'){
                        for (const key in serverError) {
                            if (serverError[key]) {
                                modalStateError += serverError[key] + '\n' ;
                            }
                        }
                    }
                    return throwError(modalStateError || serverError || 'server Error')
                }
            })
        );
    }
}

export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true
}