export class AuthResponseModel {
    constructor({ jwtToken = "", login = "", userRole = "", personId = -1 } = {}) {
        this.jwtToken = jwtToken;
        this.login = login;
        this.userRole = userRole;
        this.personId = personId;
    }
}