export class ChangePasswordRequestModel {
    constructor({ personID = 0, oldPassword = "", newPassword = "" } = {}) {
        this.personID = personID;
        this.oldPassword = oldPassword;
        this.newPassword = newPassword;
    }
}