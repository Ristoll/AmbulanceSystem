import { PersonProfileModel } from "./PersonProfileModel.js";

export class DispatcherModel extends PersonProfileModel {
    constructor({ callCount = 0, ...personProfile } = {}) {
        super(personProfile);
        this.callCount = callCount;
    }
}