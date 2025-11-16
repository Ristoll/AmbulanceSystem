// models/brigadeResourceModel.js
export class BrigadeResourceModel {
    constructor({ brigadeId = 0, brigadeType = null, totalCallsHandled = 0, distinctItemsUsed = 0, averageCallDurationMinutes = 0.0 } = {}) {
        this.brigadeId = brigadeId;
        this.brigadeType = brigadeType;
        this.totalCallsHandled = totalCallsHandled;
        this.distinctItemsUsed = distinctItemsUsed;
        this.averageCallDurationMinutes = averageCallDurationMinutes;
    }
}
