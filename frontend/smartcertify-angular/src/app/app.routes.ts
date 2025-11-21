import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { CoursesComponent } from './components/courses/courses.component';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },

    { path: 'home', component: HomeComponent },

    {
        path: 'about',
        loadComponent: () => import('./pages/about/about.component')
            .then(m => m.AboutComponent)
    },

    {
        path: 'contact-us',
        loadComponent: () => import('./pages/contact-us/contact-us.component')
            .then(m => m.ContactUsComponent)
    },
    {
        path: 'courses',
        component: CoursesComponent
    },


    { path: '**', redirectTo: 'home' }
];
