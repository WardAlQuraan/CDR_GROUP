import { Routes } from '@angular/router';
import { adminGuard } from './guards/auth.guard';

export const routes: Routes = [
  {
    path: 'admin',
    loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule),
    canActivate: [adminGuard]
  },
  {
    path: '',
    loadChildren: () => import('./client/client.module').then(m => m.ClientModule)
  },
  // { path: '**', redirectTo: '' }
];
