import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { ServiceProxyService } from '../../service/service-proxy.service';


@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private serviceProxy: ServiceProxyService
    ) { }

    canActivate() {
        if (this.serviceProxy.userLoggedIn()) {
            return true;
        }

        this.router.navigate(['/account/login'],);
        return false;
    }
}