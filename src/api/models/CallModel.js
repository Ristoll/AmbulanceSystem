import { BrigadeModel } from "./BrigadeModel.js";

export class CallModel {
    constructor({
        callId = 0,
        patientId = 0,
        dispatcherId = 0,
        hospitalId = null,
        callStatusId = null,
        phone = "",
        urgencyType = 0,
        address = "",
        assignedBrigades = [],
        startCallTime = null,
        endCallTime = null,
        arrivalTime = null,
        completionTime = null,
        callerName = "",
        patientName = "",
        dispatcherName = "",
        hospitalName = "",
        notes = "",
        estimatedArrival = "00:00:00"
    } = {}) {
        this.callId = callId;
        this.patientId = patientId;
        this.dispatcherId = dispatcherId;
        this.hospitalId = hospitalId;
        this.callStatusId = callStatusId;
        this.phone = phone;
        this.urgencyType = urgencyType;
        this.address = address;
        this.assignedBrigades = assignedBrigades.map(b => new BrigadeModel(b));
        this.startCallTime = startCallTime ? new Date(startCallTime) : null;
        this.endCallTime = endCallTime ? new Date(endCallTime) : null;
        this.arrivalTime = arrivalTime ? new Date(arrivalTime) : null;
        this.completionTime = completionTime ? new Date(completionTime) : null;
        this.callerName = callerName;
        this.patientName = patientName;
        this.dispatcherName = dispatcherName;
        this.hospitalName = hospitalName;
        this.notes = notes;
        this.estimatedArrival = estimatedArrival; // з TimeSpan можна передавати як рядок
    }
}