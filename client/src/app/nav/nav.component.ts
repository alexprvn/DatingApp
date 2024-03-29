import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';
import { Route, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(public accountService: AccountService,private router: Router,private toaster: ToastrService) { }
 
  model: any = {};



  ngOnInit(): void {
 
  }


  login() {
    this.accountService.login(this.model).subscribe(
      {
        next: _ => 
        {console.log(this.accountService.currentUser$);
          this.router.navigateByUrl('members')},
        error: error => this.toaster.error(error.error)
      });
  }



  logout() {
    this.accountService.logout();
   
  }
}
