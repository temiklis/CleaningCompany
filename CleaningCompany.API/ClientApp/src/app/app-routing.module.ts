import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { QuestionsAndAnswersComponent } from './components/questions-and-answers/questions-and-answers.component'

export const routes: Routes = [

  { path: 'questions-and-answers', component: QuestionsAndAnswersComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
