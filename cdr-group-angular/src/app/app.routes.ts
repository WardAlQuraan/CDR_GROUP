import { Routes } from '@angular/router';
import { adminGuard } from './guards/auth.guard';
import { HostHomeComponent } from './host/host-home.component';

export const routes: Routes = [
  {
    path: 'admin',
    loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule),
    canActivate: [adminGuard]
  },
  {
    path: '',
    pathMatch: 'full',
    component: HostHomeComponent
  },
  {
    path: ':companyId',
    loadChildren: () => import('./client/client.module').then(m => m.ClientModule)
  },
  // { path: '**', redirectTo: '' }
];
