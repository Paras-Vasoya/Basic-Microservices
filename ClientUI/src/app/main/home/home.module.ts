import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { HomeRoutingModule } from './home-routing.module';
import { ServiceProxyService } from '../../service/service-proxy.service';

@NgModule({
  declarations: [],
  imports: [CommonModule, ReactiveFormsModule, HomeRoutingModule],
  providers: [ServiceProxyService]
})
export class HomeModule {}
