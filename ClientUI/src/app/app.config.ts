import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { ServiceProxyService } from './service/service-proxy.service';
import { HttpClientModule } from '@angular/common/http';
import { ConfigService } from './helper/common/config.service';
import { providePrimeNG } from 'primeng/config';
import Aura from '@primeng/themes/aura';
import { provideAnimations } from '@angular/platform-browser/animations';
import { SocialAuthService } from '@abacritt/angularx-social-login';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }), 
    provideRouter(routes), 
    importProvidersFrom(HttpClientModule),
    ServiceProxyService,
    SocialAuthService,
    ConfigService,
    provideAnimations(),
    providePrimeNG({ theme: {
      preset: Aura,
      options: {
          prefix: 'p',
          darkModeSelector: 'system',
          cssLayer: false
      }
  } })
  ]
};
