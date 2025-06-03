import { NgModule } from '@angular/core';
import { provideRouter, RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './helper/common/auth.guard';
import { ServiceProxyService } from './service/service-proxy.service';
import { SocialAuthService } from '@abacritt/angularx-social-login';

export const routes: Routes = [
    {
        path: 'account',
        loadChildren: () =>
            import('./account/account.module').then((x) => x.AccountModule),
    },
    {
        path: '',
        loadChildren: () =>
            import('./main/home/home.module').then((m) => m.HomeModule),
        canActivate: [AuthGuard],
    },
    { path: '**', redirectTo: '' },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],

})
export class AppRoutingModule { }


export const APP_PROVIDERS = [
    provideRouter(routes),
    ServiceProxyService,
    SocialAuthService
  ]

