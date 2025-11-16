export class PersonProfileModel {
    constructor({
        name = "",
        surname = "",
        middleName = "",
        dateOfBirth = null,
        gender = null,
        phoneNumber = null,
        email = null,
        login = "",
        imageUrl = null
    } = {}) {
        this.name = name;
        this.surname = surname;
        this.middleName = middleName;
        this.dateOfBirth = dateOfBirth; // очікується рядок ISO або null
        this.gender = gender;
        this.phoneNumber = phoneNumber;
        this.email = email;
        this.login = login;
        this.imageUrl = imageUrl;
    }
}