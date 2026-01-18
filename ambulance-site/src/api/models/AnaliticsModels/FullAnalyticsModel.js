import { CallAnalyticsModel } from "./CallAnalyticsModel.js";
import { BrigadeResourceModel } from "./BrigadeResourceModel.js";
import { DecAllergAnalyticsModel } from "./DecAllergAnalyticsModel.js";

export class FullAnalyticsModel {
    constructor({ calls = {}, brigades = [], deceases = [] } = {}) {
        this.calls = new CallAnalyticsModel(calls);
        this.brigades = brigades.map(b => new BrigadeResourceModel(b));
        this.deceases = deceases.map(d => new DecAllergAnalyticsModel(d));
    }
}
