import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import{ Observable } from 'rxjs';
import { environment } from '../../environment/environment';
import { ChangeUserRoleRequest, ChangeUserRoleResponse } from '../Models/Change-User-Role';
import { UserProfile } from '../Models/User-Profile';

@Injectable({
  providedIn: 'root',
})
export class AdminUsersService {
  private readonly http = inject(HttpClient);

  changeRole(payload : ChangeUserRoleRequest) : Observable<ChangeUserRoleResponse>
  {
    return this.http.put<ChangeUserRoleResponse>(`${environment.apiBaseUrl}/AdminUsers/change-role`, payload);
  }

  getAllUsers():Observable<UserProfile[]>
  {
    return this.http.get<UserProfile[]>(`${environment.apiBaseUrl}/AdminUsers/listaUtenti`);
  }
}