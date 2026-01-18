import { ImageModel } from "./ImageModel.js";

export class PersonProfileModel {
    constructor({
        personId = 0,
        name = "",
        surname = "",
        middleName = "",
        dateOfBirth = null,
        gender = null,
        phoneNumber = null,
        email = null,
        login = "",
        image = null
    } = {}) {
        this.personId = personId;
        this.name = name;
        this.surname = surname;
        this.middleName = middleName;
        this.dateOfBirth = dateOfBirth;
        this.gender = gender;
        this.phoneNumber = phoneNumber;
        this.email = email;
        this.login = login;
        // че image не null перед створенням ImageModel + на всякий для гнучкості можливість зберугати не тільки модель
        this.image = image && !(image instanceof ImageModel) ? new ImageModel(image) : image;
    }
}