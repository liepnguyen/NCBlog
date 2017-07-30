import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './components/app/app.component'
import { PostItemComponent } from './components/post-item/post-item.component';
import { HomeComponent } from './components/home/home.component';
import { NavBarComponent } from './components/navbar/navbar.component';

import { WIDGET_COMPONENTS } from './components/widgets';
import { AppLayoutComponent, AdminLayoutComponent } from './shared/components/layouts';

const APP_ROUTES: Routes = [
    { path: '', component: HomeComponent },
]

const ADMIN_ROUTES: Routes = [
]

export const sharedConfig: NgModule = {
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        PostItemComponent,
        HomeComponent,
        NavBarComponent,
        AppLayoutComponent,
        AdminLayoutComponent,
        ...WIDGET_COMPONENTS
    ],
    imports: [
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: AppLayoutComponent, children: APP_ROUTES },
            { path: 'admin', component: AdminLayoutComponent, children: APP_ROUTES },
            { path: '**', redirectTo: 'home' },
        ]),
        NgbModule.forRoot()
    ]
};
