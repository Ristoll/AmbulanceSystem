export class PersonBaseModel {
    constructor({
        personId = null,
        name = "",
        surname = "",
        middleName = null,
        dateOfBirth = null,
        gender = null,
        phoneNumber = null,
        email = null
    } = {}) {
        this.personId = personId;
        this.name = name;
        this.surname = surname;
        this.middleName = middleName;
        this.dateOfBirth = dateOfBirth;
        this.gender = gender;
        this.phoneNumber = phoneNumber;
        this.email = email;
    }
}