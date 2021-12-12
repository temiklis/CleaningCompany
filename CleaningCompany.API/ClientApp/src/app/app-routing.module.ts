import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AboutUsComponent } from './components/about-us/about-us.component';
import { EmployeesComponent } from './components/admin-pages/employees/employees.component';
import { MaterialsComponent } from './components/admin-pages/materials/materials.component';
import { OrderRequestsComponent } from './components/admin-pages/order-requests/order-requests.component';
import { OrdersComponent } from './components/admin-pages/orders/orders.component';
import { ProductsComponent } from './components/admin-pages/products/products.component';
import { ClientsComponent } from './components/admin-pages/clients/clients.component';
import { UserInfoComponent } from './components/common-components/user-info/user-info.component';
import { UserOrdersComponent } from './components/common-components/user-orders/user-orders.component';
import { HomeComponent } from './components/home/home.component';
import { PricesServicesComponent } from './components/prices-services/prices-services.component';
import { ProfileComponent } from './components/profile/profile.component';
import { QuestionsAndAnswersComponent } from './components/questions-and-answers/questions-and-answers.component'
import { EditMaterialComponent } from './components/admin-pages/edit-material/edit-material.component';
import { EditProductComponent } from './components/admin-pages/edit-product/edit-product.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'questions-and-answers', component: QuestionsAndAnswersComponent },
  { path: 'about-us', component: AboutUsComponent },
  { path: 'prices-and-services', component: PricesServicesComponent },
  {
    path: 'admin', children: [
      { path: "", redirectTo: "order-requests", pathMatch: "full" },
      { path: "order-requests", component: OrderRequestsComponent },
      { path: "orders", component: OrdersComponent },
      { path: "products", component: ProductsComponent },
      { path: "materials", component: MaterialsComponent },
      { path: "clients", component: ClientsComponent },
      { path: "employees", component: EmployeesComponent },
      { path: "materials/:id", component: EditMaterialComponent },
      { path: "materials/:create", component: EditMaterialComponent },
      { path: "products/:id", component: EditProductComponent },
      { path: "products/create", component: EditProductComponent },
    ]
  },
  {
    path: 'profile', component: ProfileComponent, children: [
      { path: '', redirectTo: 'info', pathMatch: "full" },
      { path: 'info', component: UserInfoComponent },
      { path: 'orders', component: UserOrdersComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes),
    CommonModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
