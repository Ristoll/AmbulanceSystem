// models/callAnalyticsModel.js
export class CallAnalyticsModel {
    constructor({ totalCalls = 0, completedCalls = 0, averageResponseMinutes = 0.0, callsByUrgency = {} } = {}) {
        this.totalCalls = totalCalls;
        this.completedCalls = completedCalls;
        this.averageResponseMinutes = averageResponseMinutes;
        this.callsByUrgency = callsByUrgency; // об'єкт у форматі {urgencyType: count}
    }
}
