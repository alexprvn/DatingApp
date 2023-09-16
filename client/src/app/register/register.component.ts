
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};
  @Output() cancelRegister = new EventEmitter();
  constructor(private accoutService:AccountService,private toaster:ToastrService)
  { }

  ngOnInit(): void {
  }

  register() {
    this.accoutService.register(this.model).subscribe(
      {
        next:()=>
        {
          
          this.cancel();
        },
        error:error => this.toaster.error(error.error)       
      }
    )
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

}
