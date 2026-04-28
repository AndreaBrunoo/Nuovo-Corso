import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Auth } from '../../services/auth.service';

@Component({
  selector: 'app-dashboard-page',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './dashboard.page.html',
})
export class DashboardPage {
  private readonly authService = inject(Auth);

  readonly user = this.authService.currentUser;

  canEditInterests(): boolean {
    return this.authService.hasAnyRole(['Admin', 'Editor']);
  }

  isAdmin(): boolean {
    return this.authService.hasRole('Admin');
  }
}