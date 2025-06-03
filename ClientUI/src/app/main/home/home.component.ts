import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ServiceProxyService } from '../../service/service-proxy.service';
import { ResponseModel } from '../../helper/common/models/responseModel';

@Component({
  selector: 'app-home',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  standalone: true
})
export class HomeComponent implements OnInit {
  products: any;

  constructor(
    private router: Router, private serviceProxy: ServiceProxyService,
  ) {
  }

  ngOnInit(): void {
    this.serviceProxy.getProductList().subscribe({
      next: (res: ResponseModel) => {
        if (res != null) {
          this.products = res;
        }
      },
      error: (err) => { },
    });
  }

  logOut() {
    localStorage.removeItem('token');
    this.router.navigate(['/account/login']);
  }
}
