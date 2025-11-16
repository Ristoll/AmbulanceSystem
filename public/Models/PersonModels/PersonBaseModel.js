export class PersonBaseModel {
    constructor({
        name = "",
        surname = "",
        middleName = null,
        dateOfBirth = null,  // зберігати як ISO string або Date
        gender = null,
        phoneNumber = null,
        email = null,
        imageUrl = null
    } = {}) {
        this.name = name;
        this.surname = surname;
        this.middleName = middleName;
        this.dateOfBirth = dateOfBirth ? new Date(dateOfBirth) : null;
        this.gender = gender;
        this.phoneNumber = phoneNumber;
        this.email = email;
        this.imageUrl = imageUrl;
    }
}