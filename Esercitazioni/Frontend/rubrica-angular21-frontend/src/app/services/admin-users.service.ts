import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ChangeUserRoleRequest, ChangeUserRoleResponse } from '../Models/Change-User-Role.model';

@Injectable({providedIn: 'root',})
export class AdminUsersService {
  private readonly http = inject(HttpClient);

  changeRole(payload: ChangeUserRoleRequest): Observable<ChangeUserRoleResponse> {
    return this.http.put<ChangeUserRoleResponse>(`${environment.apiBaseUrl}/AdminUsers/change-role`, payload);
  }
}