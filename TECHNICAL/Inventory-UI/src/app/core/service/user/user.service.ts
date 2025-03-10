import { Injectable } from '@angular/core';
import { APIConstant } from '../../constant/APIConstant';
import { environment } from '../../../../environments/environment';
import { MasterService } from '../master/master.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private masterService: MasterService) { }



  LoginUser(val:any){
    return this.masterService.post(environment.api+APIConstant.Users.userlogin,val)

  }
}
