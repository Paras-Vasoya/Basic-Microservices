import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
    HttpHeaders,
    HttpResponse,
} from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
@Injectable()
export class AppInterceptor implements HttpInterceptor {
    constructor(private router: Router, private messageService: MessageService) { }

    intercept(
        request: HttpRequest<unknown>,
        next: HttpHandler
    ): Observable<HttpEvent<unknown>> {
        let newHeaders = new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${localStorage.getItem('token')}`
        });
        return next.handle(request).pipe(
            map((event: HttpEvent<any>) => {
                if (event instanceof HttpResponse) {
                    if (event.ok) {
                        if (event.body.success) {
                            this.messageService.add({
                                severity: 'success',
                                summary: 'Success',
                                detail: event.body.message,
                            });
                        } else {
                            this.messageService.add({
                                severity: 'error',
                                summary: 'Error',
                                detail: event.body.message,
                            });
                        }
                    } else {
                        console.log('Error');
                    }
                }

                return event;
            })
        );
    }
}
