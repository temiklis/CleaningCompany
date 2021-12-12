import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import * as moment from 'moment';
import { UpdateUserProfile } from '../../../models/interfaces/Users/UpdateUserProfile';
import { UserProfile } from '../../../models/interfaces/Users/UserProfile';
import { HelperService } from '../../../services/helper.service';
import { UsersService } from '../../../services/users.service';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss']
})
export class UserInfoComponent implements OnInit {

  userProfile: UserProfile = {
    Address: null,
    Birthday: null,
    Email: null,
    FirstName: null,
    Gender: null,
    LastName: null
  }

  genders: Gender[] = [];
  minDate: NgbDateStruct;

  userProfileForm: FormGroup;

  constructor(private userService: UsersService,
    private helperService: HelperService,
    private fb: FormBuilder) { }

  ngOnInit(): void {
    this.getUserProfile();
  }

  getUserProfile() {
    this.userService.getUserProfile().then(userProfile => {
      this.userProfile = userProfile;
      this.fillForm();
    })
  }

  fillForm() {
    var birthdayDate = this.userProfile.Birthday
      ? new Date(moment(this.userProfile.Birthday).format('MM-DD-YYYY'))
      : null;

    var datepickerDate = birthdayDate
      ? { year: birthdayDate.getFullYear(), month: birthdayDate.getMonth() + 1, day: birthdayDate.getDate() }
      : null;

    this.userProfileForm = this.fb.group({
      FirstName: this.fb.control(this.userProfile.FirstName),
      LastName: this.fb.control(this.userProfile.LastName),
      Address: this.fb.control(this.userProfile.Address),
      Birthday: this.fb.control(datepickerDate),
      Email: this.fb.control({ value: this.userProfile.Email, disabled: true })
    })

    this.genders = this.getGenderList();
    this.minDate = { year: 1930, month: 1, day: 1 };
  }

  getGenderList(): Gender[] {
    let genders = [
      { Id: 0, Name: 'Male', IsChecked: false },
      { Id: 1, Name: 'Female', IsChecked: false },
      { Id: 2, Name: 'Not specified', IsChecked: false },
    ];

    genders.every(gender => {
      if (gender.Id == this.userProfile.Gender) {
        gender.IsChecked = true;
        return false;
      }

      return true;
    })

    return genders;
  }

  save() {
    debugger;
    var userProfile = this.userProfileForm.getRawValue() as UserProfile;

    let date = userProfile.Birthday as any;
    var birthday = date
      ? moment(new Date(`${date.month}-${date.day}-${date.year}`)).format('YYYY-MM-DD')
      : null;

    let updatedProfile: UpdateUserProfile = { ...this.userProfileForm.getRawValue(), Birthday: new Date(birthday), Gender: this.userProfile.Gender };

    this.userService.updateUserProfile(updatedProfile).then(result => {
      this.userProfile = { ...userProfile, Birthday: new Date(birthday), Gender: this.userProfile.Gender };
      this.helperService.alert("Your profile was successfully updated!");
    })
  }

  genderChanged(index: number) {
    this.genders.forEach((gender, i) => {
      if (i != index) {
        gender.IsChecked = false;
      }
      else {
        gender.IsChecked = true;
        this.userProfile.Gender = gender.Id;
      }
    })
  }

}

export interface Gender {
  Name: string;
  Id: number;
  IsChecked: boolean;
}
