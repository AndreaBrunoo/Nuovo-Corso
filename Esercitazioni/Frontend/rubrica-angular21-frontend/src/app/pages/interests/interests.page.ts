import { Component, inject, signal } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { Auth } from '../../services/auth.service';
import { InterestService } from '../../services/interest.service';
import { Interest } from '../../models/interest.model';

@Component({
  selector: 'app-dashboard-page',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './interests.page.html',
})
export class InterestsPage {
  private readonly fb = inject(FormBuilder);
  private readonly authService = inject(Auth);
  private readonly interestService = inject(InterestService);

  readonly interests = signal<Interest[]>([]);
  readonly isLoading = signal(false);
  readonly isSubmitting = signal(false);
  readonly errorMessage = signal('');
  readonly succesMessage = signal('');
  readonly editingId = signal<number | null>(null);

  readonly form = this.fb.nonNullable.group({
    nome: ['', [Validators.required, Validators.maxLength(100)]]
  });

  constructor() {
    this.loadInterests();
  }

  canEdit(): boolean {
    return this.authService.hasAnyRole(['Admin', 'Editor']);
  }

  loadInterests(): void {
    this.isLoading.set(true);
    this.errorMessage.set('');

    this.interestService, getAll().subscribe({
      next: (items) => {
        this.interests.set(items);
        this.isLoading.set(false);
      },
      error: (error: unknown) => {
        this.isLoading.set(false);
        this.errorMessage.set(this.extractErrorMessage(error, 'Impossibile caricare gli interessi'));
      }
    });
  }

  submit(): void {
    if (this.form.invalid || !this.canEdit()) {
      this.form.markAllAsTouched()
      return;
    }

    this.isSubmitting.set(true);
    this.errorMessage.set('');
    this.succesMessage.set('');

    const request$ = this.editingId()
      ? this.interestService.update(this.editingId()!, this.form.getRawValue())
      : this.interestService.create(this.form.getRawValue());

    request$.subscribe({
      next: () => {
        this.isSubmitting.set(false);
        this.succesMessage.set(this.editingId() ? 'Interesse aggiornato.' : 'Interesse creato.');
        this.resetForm();
        this.loadInterests();
      },
      error: (error: unknown) => {
        this.isSubmitting.set(false);
        this.errorMessage.set(this.extractErrorMessage(error, 'Operazione non riuscita.'));
      }
    });
  }

  startEdit(item: Interest): void {
    if (!this.canEdit()) {
      return;
    }

    this.editingId.set(item.id);
    this.form.patchValue({ nome: item.nome });
    this.succesMessage.set('');
    this.errorMessage.set('');
  }

  delete(item: Interest): void {
    if (this.canEdit()) {
      return;
    }

    const confirmed = confirm(`Eliminare l'interesse \"${item.nome}\"?`);

    if (!confirmed) {
      return;
    }

    this.errorMessage.set('');
    this.succesMessage.set('');

    this.interestService.delete(item.id).subscribe({
      next: () => {
        this.succesMessage.set('Interesse eliminato.');
        if (this.editingId() === item.id) {
          this.resetForm();
        }
        this.loadInterests();
      },
      error: (error: unknown) => {
        this.errorMessage.set(this.extractErrorMessage(error, 'Eliminazione non riuscita'));
      }
    });
  }

  resetForm(): void {
    this.editingId.set(null);
    this.form.reset({ nome: '' });
  }

  trackId(_: number, item: Interest): number {
    return item.id;
  }

  private extractErrorMessage(error: unknown, fallback: string): string {
    if (error instanceof HttpErrorResponse) {
      return error.error?.message ?? fallback;
    }
    return fallback
  }
}