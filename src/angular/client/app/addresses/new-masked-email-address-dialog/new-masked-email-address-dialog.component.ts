import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MaskedEmail, MaskedEmailRequest, Address } from '../../shared/models/model';
import { AddressService } from '../../shared/services/address.service';
import { HashService } from '../../shared/services/hash.service';
import { ClipboardService } from '../../shared/services/clipboard.service';
@Component({
  selector: 'app-new-masked-email-address-dialog',
  templateUrl: './new-masked-email-address-dialog.component.html',
  styleUrls: ['./new-masked-email-address-dialog.component.scss']
})
export class NewMaskedEmailAddressDialogComponent implements OnInit {
  public addressForm = new FormGroup({
    name: new FormControl(''),
    description: new FormControl(''),
    password: new FormControl(''),
  });
  public hidePassword: boolean = true;
  public timeLeft: number = 45;
  public interval;
  public showGeneratedPassword: boolean = false;
  public addressCreated: Address;

  constructor(public dialogRef: MatDialogRef<NewMaskedEmailAddressDialogComponent>,
    private addressService: AddressService,
    private hashService: HashService,
    private formBuilder: FormBuilder,
    private clipboard: ClipboardService,
    @Inject(MAT_DIALOG_DATA) public data: { addresses: MaskedEmail[] }) {
    this.addressForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: [''],
      password: ['']
    });
  }

  ngOnInit() {
  }

  public close(): void {
    this.dialogRef.close();
  }

  public managePasswordVisibility(): void {
    this.hidePassword = !this.hidePassword;
  }

  public createAddress(): void {
    var request: MaskedEmailRequest = {
      name: this.addressForm.get('name').value,
      description: this.addressForm.get('description').value,
      forwardingEnabled: true
    };
    if (this.addressForm.get('password').value.length > 0) {
      const passwordHash = this.hashService.hashPassword(this.addressForm.get('password').value);
      request.passwordHash = passwordHash;
    }
    this.addressService.createAddress(request)
      .subscribe(address => {
        this.addressCreated = address;
        if (this.addressForm.get('password').value.length === 0) {
          this.addressForm.controls['password'].setValue(address.password);
          this.copyToClipboard(address.password);

          this.startTimer(() => this.closeDialogRefAfterCreate());

        } else {
          this.closeDialogRefAfterCreate();
        }
      });

  }

  public getErrorMessage() {
    return this.addressForm.get('name').hasError('required') ? 'You must enter a value' :
      '';
  }

  public closeDialogRefAfterCreate() {
    this.dialogRef.close({ event: 'Create', data: MaskedEmail.fromAddress(this.addressCreated) });
  }

  private startTimer(callback: Function) {
    this.showGeneratedPassword = true;
    this.interval = setInterval(() => {
      if (this.timeLeft > 0) {
        this.timeLeft--;
      } else {
        this.timeLeft = 45;
        callback();
      }
    }, 1000)
  }

  private copyToClipboard(text: string): void {
    this.clipboard.copyToClipboard(
      text,
      'The generated password has been copied in your clipboard!'
    );
  }
}
