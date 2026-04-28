import { Component, inject, signal } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { AdminUsersService } from '../../services/admin-users.service';

@Component({
  selector: 'app-admin-change-role-page',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './admin-change-role.page.html',
})
export class AdminChangeRolePage {
  private readonly fb = inject(FormBuilder);
  private readonly adminUsers = inject(AdminUsersService);

  readonly isSubmitting = signal(false);
  readonly succesMessage = signal('');
  readonly errormessage = signal('');
  readonly roles = ['Admin', 'Editor', 'User'];

  readonly form = this.fb.nonNullable.group({
    email: ['', [Validators.required, Validators.email]],
    newrole: ['User', [Validators.required]]
  });

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting.set(true);
    this.succesMessage.set('');
    this.errormessage.set('');

    this.adminUsers.changeRole(this.form.getRawValue()).subscribe({
      next: (response) => {
        this.isSubmitting.set(false);
        this.succesMessage.set(`${response.message} Nuovo Ruolo: ${response.role}`);
      },
      error: (error: unknown) => {
        this.isSubmitting.set(false);
        this.errormessage.set(this.extractErrorMessage(error));
      }
    });
  }

  private extractErrorMessage(error: unknown): string {
    if (error instanceof HttpErrorResponse) {
      return error.error?.message ?? 'Cambio ruolo non riuscito.';
    }

    return 'Cambio ruolo non riuscito'
  }
}