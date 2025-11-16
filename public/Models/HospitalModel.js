import { BrigadeModel } from "./BrigadeModel.js";
import { CallModel } from "./CallModel.js";

export class HospitalModel {
    constructor({
        hospitalId = 0,
        name = "",
        latitude = null,
        longitude = null,
        note = "",
        brigades = [],
        calls = []
    } = {}) {
        this.hospitalId = hospitalId;
        this.name = name;
        this.latitude = latitude;
        this.longitude = longitude;
        this.note = note;
        this.brigades = brigades.map(b => new BrigadeModel(b));
        this.calls = calls.map(c => new CallModel(c));
    }
}