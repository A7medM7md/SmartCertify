import { CommonModule } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, signal } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-contact-us',
  imports: [FormsModule, CommonModule, ReactiveFormsModule],
  templateUrl: './contact-us.component.html',
  styleUrl: './contact-us.component.css'
})
export class ContactUsComponent implements OnInit {
  contactForm!: FormGroup;
  formValidity = signal(false);
  errorMessage = signal(''); // signal for general error message
  fieldErrors = signal<Record<string, string>>({}); // signal for field-specific errors
  ObjectValues = Object.values;

  constructor(private fb: FormBuilder) {
    this.contactForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      subject: ['', Validators.required],
      message: ['', Validators.required],
    });

    this.contactForm.statusChanges.subscribe({
      next: (status) => {
        this.formValidity.set(status === 'VALID');
      },
    })

    Object.keys(this.contactForm.controls).forEach((field) => {
      const control = this.contactForm.get(field);
      control?.statusChanges.subscribe(() => this.updateFieldErrors());
    });

  }

  // Update field-specific errors dynamically
  updateFieldErrors() {
    const errors: Record<string, string> = {};
    Object.keys(this.contactForm.controls).forEach((field) => {
      const control = this.contactForm.get(field);

      if (control?.touched || control?.dirty) {
        if (control?.hasError('required')) {
          errors[field] = `${field} is required.`;
        }
        else if (control?.hasError('email')) {
          errors[field] = 'Invalid email address.';
        }
      }
    });
    this.fieldErrors.set(errors); // update signal with current errors..
  }

  ngOnInit(): void {
  }

  triggerError() {
    // Simulate a non-HTTP error
    throw new Error('This is a simulated error');
  }

  onSubmit() {
    if (this.contactForm.valid) {
      //   // Call the API service
      //   this.contactService.sendMessage(this.contactForm.value).subscribe({
      //     next: () => {
      //       // Reset form and clear errors
      //       this.contactForm.reset();
      //       this.errorMessage.set('');          // Clear general error
      //       this.fieldErrors.set({});           // Clear field-specific errors
      //     },
      //     error: (error: HttpErrorResponse) => {
      //       // Handle different HTTP status codes
      //       if (error.status === 404) {
      //         this.errorMessage.set('The requested resource was not found.');
      //       } else if (error.status === 400) {
      //         this.errorMessage.set('Bad request. Please check your inputs.');
      //       } else {
      //         this.errorMessage.set('An error occurred. Please try again later.');
      //       }
      //     }
      //   });
      // } else {
      //   // Form is invalid → update field-specific errors
      //   this.updateFieldErrors();
      // }
    }

  }
}
