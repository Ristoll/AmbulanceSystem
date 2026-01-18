export class PatientCreateRequestModel {
    constructor({
        name = "",
        surname = "",
        middleName = null,
        phoneNumber = "",
        gender = null,
        dateOfBirth = null
    } = {}) {
        this.name = name;
        this.surname = surname;
        this.middleName = middleName;
        this.phoneNumber = phoneNumber;
        this.gender = gender;
        this.dateOfBirth = dateOfBirth;
    }
}
