import { Routes, RouterModule } from '@angular/router';
import { ClientLayoutComponent } from './client.layout.component';

const routes: Routes = [
  { path: '', component: ClientLayoutComponent},
];

export const ClientRoutes = RouterModule.forChild(routes);
