import { PersonBaseModel } from "./PersonBaseModel.js";

export class PersonUpdateModel extends PersonBaseModel {
    constructor({ personId = 0, roleId = null, ...rest } = {}) {
        super(rest); // всі поля з PersonBase
        this.personId = personId;
        this.roleId = roleId;
    }
}