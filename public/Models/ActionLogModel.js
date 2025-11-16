export class ActionLogModel {
    constructor({ actionLogId = 0, personId = 0, firstName = null, secondName = null, action = null } = {}) {
        this.actionLogId = actionLogId;
        this.personId = personId;
        this.firstName = firstName;
        this.secondName = secondName;
        this.action = action;
    }
}