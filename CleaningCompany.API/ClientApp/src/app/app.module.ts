import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpService } from './services/http.service';
import { AppRoutingModule, routes } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/common-components/nav-menu/nav-menu.component';
import { AppFooterComponent } from './components/common-components/app-footer/app-footer.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthorizationModule } from './authorization.module';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthorizeInterceptor } from './services/authorization/authorize.interceptor';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { GoogleMapsModule } from '@angular/google-maps';
import { QuestionsAndAnswersComponent } from './components/questions-and-answers/questions-and-answers.component';
import { AboutUsComponent } from './components/about-us/about-us.component';
import { HomeComponent } from './components/home/home.component';
import { PricesServicesComponent } from './components/prices-services/prices-services.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CreateOrderRequestUserInfoModalComponent } from './components/modals/create-order-request-user-info-modal/create-order-request-user-info-modal.component';
import { OrderRequestsComponent } from './components/admin-pages/order-requests/order-requests.component';
import { OrdersComponent } from './components/admin-pages/orders/orders.component';
import { UsersComponent } from './components/admin-pages/users/users.component';
import { EmployeesComponent } from './components/admin-pages/employees/employees.component';
import { ProductsComponent } from './components/admin-pages/products/products.component';
import { MaterialsComponent } from './components/admin-pages/materials/materials.component';
import { AdminNavMenuComponent } from './components/admin-pages/admin-nav-menu/admin-nav-menu.component';
import { SidenavComponent } from './components/admin-pages/sidenav/sidenav.component';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ProfileComponent } from './components/profile/profile.component';
import { UserInfoComponent } from './components/common-components/user-info/user-info.component';
import { UserOrdersComponent } from './components/common-components/user-orders/user-orders.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    AppFooterComponent,
    QuestionsAndAnswersComponent,
    AboutUsComponent,
    HomeComponent,
    PricesServicesComponent,
    CreateOrderRequestUserInfoModalComponent,
    SidenavComponent,
    OrderRequestsComponent,
    OrdersComponent,
    UsersComponent,
    EmployeesComponent,
    ProductsComponent,
    MaterialsComponent,
    AdminNavMenuComponent,
    ProfileComponent,
    UserInfoComponent,
    UserOrdersComponent,
  ],
  imports: [
    RouterModule.forRoot(routes),
    AppRoutingModule,
    BrowserModule,
    FontAwesomeModule,
    AuthorizationModule,
    BrowserAnimationsModule,
    GoogleMapsModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
