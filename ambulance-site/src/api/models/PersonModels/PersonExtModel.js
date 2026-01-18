import { PersonBaseModel } from "./PersonBaseModel.js";

export class PersonExtModel extends PersonBaseModel {
    constructor({ login = "", role = "", ...baseProps } = {}) {
        super(baseProps);
        this.login = login;
        this.role = role;
    }
}