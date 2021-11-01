import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AboutUsComponent } from './components/about-us/about-us.component';
import { HomeComponent } from './components/home/home.component';
import { PricesServicesComponent } from './components/prices-services/prices-services.component';
import { QuestionsAndAnswersComponent } from './components/questions-and-answers/questions-and-answers.component'

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'questions-and-answers', component: QuestionsAndAnswersComponent },
  { path: 'about-us', component: AboutUsComponent },
  { path: 'prices-and-services', component: PricesServicesComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
