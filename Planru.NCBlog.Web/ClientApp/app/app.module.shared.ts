import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './components/app/app.component'
import { BlogItemComponent } from './components/blog-item/blog-item.component';
import { CounterComponent } from './components/counter/counter.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { HomeComponent } from './components/home/home.component';
import { NavBarComponent } from './components/navbar/navbar.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';

import { WIDGET_COMPONENTS } from './components/widgets';

export const sharedConfig: NgModule = {
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        BlogItemComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        NavBarComponent,
        NavMenuComponent,
        ...WIDGET_COMPONENTS
    ],
    imports: [
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'home' }
        ]),
        NgbModule.forRoot()
    ]
};
