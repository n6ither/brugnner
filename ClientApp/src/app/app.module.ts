import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NbEvaIconsModule } from '@nebular/eva-icons';
import {
  NbAccordionModule,
  NbButtonModule,
  NbCardModule,
  NbCheckboxModule,
  NbDialogModule,
  NbIconModule,
  NbListModule,
  NbProgressBarModule,
  NbTabsetModule,
  NbToastrModule,
  NbToastrService,
  NbToggleModule,
  NbTooltipModule,
  NbWindowModule,
} from '@nebular/theme';
import { ScrollToModule } from '@nicky-lenaers/ngx-scroll-to';

import { AboutMeComponent } from './about-me/about-me.component';
import { AdminModule } from './admin/admin.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthModule } from './auth/auth.module';
import { ConfirmComponent } from './confirm/confirm.component';
import { ErrorModule } from './error/error.module';
import { HomeComponent } from './home/home.component';
import { ShortcutsComponent } from './home/shortcuts/shortcuts.component';
import { ApiUrlInterceptor } from './interceptors/api-url.interceptor';
import { HttpErrorInterceptor } from './interceptors/http-error.interceptor';
import { JWTAuthInterceptor } from './interceptors/jwt-auth.interceptor';
import { LoggingService } from './logging/logging.service';
import { PostsModule } from './posts/posts.module';
import { SearchComponent } from './search/search.component';
import { ThemeModule } from './theme/theme.module';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ShortcutsComponent,
    AboutMeComponent,
    ConfirmComponent,
    SearchComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    ThemeModule.forRoot(),
    NbDialogModule.forRoot(),
    NbWindowModule.forRoot(),
    NbToastrModule.forRoot(),
    NbButtonModule,
    NbCardModule,
    NbListModule,
    NbToggleModule,
    NbAccordionModule,
    NbProgressBarModule,
    NbIconModule,
    NbEvaIconsModule,
    ScrollToModule.forRoot(),
    NbTabsetModule,
    NbTooltipModule,
    NbCheckboxModule,
    AuthModule,
    ErrorModule,
    PostsModule,
    AdminModule
  ],
  bootstrap: [AppComponent],
  providers: [
    NbToastrService,
    LoggingService,
    { provide: HTTP_INTERCEPTORS, useClass: ApiUrlInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JWTAuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true }
  ],
  entryComponents: [
    ConfirmComponent
  ]
})
export class AppModule {

}