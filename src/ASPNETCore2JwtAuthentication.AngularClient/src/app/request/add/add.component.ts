import { Component, OnInit } from '@angular/core';
import { Request } from './../models/request';
import { Router } from "@angular/router";
import { NgForm } from "@angular/forms";
import { RequestService} from "./../services/request.service";
import {  HttpErrorResponse } from "@angular/common/http";

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {
  error="";

  model: Request = {
    id:0,
    title: "",
    processId: 0,
    dateRequested: "",
    userId: 0,
    currentStateId: 0,
    body:""
  };


  constructor(private router: Router,private requestService:RequestService) { }

  ngOnInit() { }

  submitForm(form: NgForm) {

  console.log(this.model);
    console.log(form.value);
    this.model.processId =2;

    this.requestService.add(this.model)
      .subscribe(() => {
        this.router.navigate(["/welcome"]);
      }, (error: HttpErrorResponse) => {
        console.error("ChangePassword error", error);
        this.error = `${error.error} -> ${error.statusText}: ${error.message}`;
      });
    
  }
}
