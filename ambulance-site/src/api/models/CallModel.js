import { BrigadeModel } from "./BrigadeModel.js";

export class CallModel {
    constructor({
        callId = -1,
        patientId = null,
        dispatcherId = -1,
        hospitalId = null,

        phone = "",
        urgencyType = 0,
        address = "",
        notes = "",

        startCallTime = null,
        endCallTime = null,
        arrivalTime = null,
        completionTime = null,

        assignedBrigades = [],

        patientFullName = "",
        dispatcherIndentificator = "",
        hospitalName = "",

        estimatedArrival = null
    } = {}) {
        this.callId = callId;
        this.patientId = patientId;
        this.dispatcherId = dispatcherId;
        this.hospitalId = hospitalId;

        this.phone = phone;
        this.urgencyType = urgencyType;
        this.address = address;
        this.notes = notes;

        this.startCallTime = startCallTime ? new Date(startCallTime) : null;
        this.endCallTime = endCallTime ? new Date(endCallTime) : null;
        this.arrivalTime = arrivalTime ? new Date(arrivalTime) : null;
        this.completionTime = completionTime ? new Date(completionTime) : null;

        this.assignedBrigades = assignedBrigades.map(b => new BrigadeModel(b));

        // Додаткові поля тільки для UI
        this.patientFullName = patientFullName;
        this.dispatcherIndentificator = dispatcherIndentificator;
        this.hospitalName = hospitalName;

        // TimeSpan приходить рядком — зберігаємо як є
        this.estimatedArrival = estimatedArrival;
    }
}
