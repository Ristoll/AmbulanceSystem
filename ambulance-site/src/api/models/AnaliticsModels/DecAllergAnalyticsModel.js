// models/decAllergAnalyticsModel.js
export class DecAllergAnalyticsModel {
    constructor({ deceaseStatistics = {}, allergyStatistics = {} } = {}) {
        // Об'єкти у форматі {назва: кількість}
        this.deceaseStatistics = deceaseStatistics;
        this.allergyStatistics = allergyStatistics;
    }
}
