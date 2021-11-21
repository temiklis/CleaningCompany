import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AboutUsComponent } from './components/about-us/about-us.component';
import { EmployeesComponent } from './components/admin-pages/employees/employees.component';
import { MaterialsComponent } from './components/admin-pages/materials/materials.component';
import { OrderRequestsComponent } from './components/admin-pages/order-requests/order-requests.component';
import { OrdersComponent } from './components/admin-pages/orders/orders.component';
import { ProductsComponent } from './components/admin-pages/products/products.component';
import { UsersComponent } from './components/admin-pages/users/users.component';
import { HomeComponent } from './components/home/home.component';
import { PricesServicesComponent } from './components/prices-services/prices-services.component';
import { QuestionsAndAnswersComponent } from './components/questions-and-answers/questions-and-answers.component'

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'questions-and-answers', component: QuestionsAndAnswersComponent },
  { path: 'about-us', component: AboutUsComponent },
  { path: 'prices-and-services', component: PricesServicesComponent },
  {
    path: 'admin', component: HomeComponent, children: [
      { path: "", redirectTo: "order-requests", pathMatch: "full" },
      { path: "order-requests", component: OrderRequestsComponent },
      { path: "orders", component: OrdersComponent },
      { path: "products", component: ProductsComponent },
      { path: "materials", component: MaterialsComponent },
      { path: "users", component: UsersComponent },
      { path: "employees", component: EmployeesComponent },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
