import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersRoutingModule } from './users-routing.module';
import { UsersComponent } from './users.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { PageHeaderModule } from '../../shared/modules';

@NgModule({
  imports: [
    CommonModule,
    UsersRoutingModule,
    NgxDatatableModule,
    PageHeaderModule
  ],
  declarations: [UsersComponent]
})
export class UsersModule { }
