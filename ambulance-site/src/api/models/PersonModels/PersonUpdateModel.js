import { PersonBaseModel } from "./PersonBaseModel.js";
import { ImageModel } from "./ImageModel.js";

export class PersonUpdateModel extends PersonBaseModel {
    constructor({ role = null, login = null, image = null, ...rest } = {}) {
        super(rest);
        this.role = role;
        this.login = login;
        // чи image не null перед створенням ImageModel + на всякий для гнучкості можливість зберугати не тільки модель
        this.image = image && !(image instanceof ImageModel) ? new ImageModel(image) : image;
}
}