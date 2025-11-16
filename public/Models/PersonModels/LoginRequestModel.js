export class LoginRequestModel {
    constructor({ login = "", password = "" } = {}) {
        this.login = login;
        this.password = password;
    }
}