import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from "@angular/forms";
import { RequestRoutingModule } from './request-routing.module';
import { AddComponent } from './add/add.component';
import {RequestService} from "./services/request.service";

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RequestRoutingModule
  ],
  declarations: [AddComponent],
  providers: [RequestService]
})
export class RequestModule { }
