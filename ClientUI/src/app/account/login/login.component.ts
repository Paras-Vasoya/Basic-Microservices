import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ServiceProxyService } from '../../service/service-proxy.service';
import { ResponseModel } from '../../helper/common/models/responseModel';
import { CommonModule } from '@angular/common';
import { PasswordModule } from 'primeng/password';
import { ButtonModule } from 'primeng/button';
import { FloatLabelModule } from 'primeng/floatlabel';
import { InputTextModule } from 'primeng/inputtext';
import { FacebookLoginProvider, SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { SocialLoginInputModel } from '../../helper/common/models/socilLoginInput';
declare const google: any;
@Component({
  selector: 'app-login',
  imports: [CommonModule, PasswordModule, ReactiveFormsModule, ButtonModule, FloatLabelModule, InputTextModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  standalone: true
})
export class LoginComponent implements OnInit {
  form!: FormGroup;
  submitted!: boolean;

  constructor(private fb: FormBuilder,
    private router: Router,
    private serviceProxy: ServiceProxyService,
    private socialAuthService: SocialAuthService) {
  }

  ngOnInit(): void {
    this.initializeForm();
    this.googleViewInit();
    google.accounts.id.renderButton(document.getElementById('googleBtn'), {
      theme: 'outline',
      size: 'large',
    });
  }

  initializeForm() {
    this.form = this.fb.group({
      username: [null, Validators.required],
      password: [null, Validators.required],
    });
  }

  submit() {
    if (!this.form.valid) {
      this.submitted = true;
      return;
    }

    this.serviceProxy.login(this.form.value).subscribe({
      next: (res: ResponseModel) => {
        if (res.success) {
          this.storeTokenAndGoToHomePage(res.data?.token);
        }
      },
      error: (err) => { },
    });
  }

  googleViewInit(): void {
    google.accounts.id.initialize({
      client_id: '392404429332-dajs1kebcvbfntnbtq2830aud3st5euv.apps.googleusercontent.com',
      scope: 'openid profile email',
      callback: (credentialResponse: any) => {
        let input: SocialLoginInputModel = {
          idToken: credentialResponse.credential,
        };
        this.serviceProxy.loginWithGoogle(input).subscribe({
          next: (response: ResponseModel) => {
            if (response.success) {
              this.storeTokenAndGoToHomePage(response.data?.token);
            }
          },
          error: (err) => { },
        });
      },
    });


  }

  loginWithFacebook() {
    this.socialAuthService
      .signIn(FacebookLoginProvider.PROVIDER_ID)
      .then((res: SocialUser) => {
        if (res) {
          let input: SocialLoginInputModel = {
            idToken: res.authToken,
          };
          this.serviceProxy.loginWithFacebook(input).subscribe({
            next: (response: ResponseModel) => {
              if (response.success) {
                this.storeTokenAndGoToHomePage(response.data?.token);
              }
            },
          });
        }
      });
  }

  storeTokenAndGoToHomePage(token: string) {
    localStorage.setItem('token', token);
    this.router.navigate(['']);
  }
}
