
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};
  @Output() cancelRegister = new EventEmitter();
  constructor(private accoutService:AccountService)
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
        error:error => console.log(error)

        
      }
    )
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

}
