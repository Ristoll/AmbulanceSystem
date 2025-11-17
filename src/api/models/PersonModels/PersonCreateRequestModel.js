import { PersonBaseModel } from "./PersonBaseModel.js";

export class PersonCreateRequestModel extends PersonBaseModel {
    constructor({ login = "", password = "", role = null, ...baseProps } = {}) {
        super(baseProps);
        this.login = login;
        this.password = password;
        this.role = role;
    }
}