import { BrigadeMemberModel } from "./BrigadeMemberModel.js";

export class BrigadeModel {
    constructor({
        brigadeId = 0,
        hospitalId = 0,
        brigadeStateId = 0,
        brigadeTypeId = 0,
        currentCallId = null,
        brigadeStateName = "",
        brigadeTypeName = "",
        latitude = null,
        longitude = null,
        estimatedArrival = "00:00:00", // з TimeSpan можна передавати як рядок
        estimatedDistanceKm = 0,
        members = []
    } = {}) {
        this.brigadeId = brigadeId;
        this.hospitalId = hospitalId;
        this.brigadeStateId = brigadeStateId;
        this.brigadeTypeId = brigadeTypeId;
        this.currentCallId = currentCallId;
        this.brigadeStateName = brigadeStateName;
        this.brigadeTypeName = brigadeTypeName;
        this.latitude = latitude;
        this.longitude = longitude;
        this.estimatedArrival = estimatedArrival;
        this.estimatedDistanceKm = estimatedDistanceKm;
        this.members = members.map(m => new BrigadeMemberModel(m));
    }
}