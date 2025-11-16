export class AuthResponseModel {
    constructor({ jwtToken = "", login = "", userRole = "" } = {}) {
        this.jwtToken = jwtToken;
        this.login = login;
        this.userRole = userRole;
    }
}