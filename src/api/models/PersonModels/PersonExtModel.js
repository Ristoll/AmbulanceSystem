import { PersonBaseModel } from "./PersonBaseModel.js";

export class PersonExtModel extends PersonBaseModel {
    constructor({ personId = 0, login = "", role = "", ...baseProps } = {}) {
        super(baseProps);
        this.personId = personId;
        this.login = login;
        this.role = role;
    }
}