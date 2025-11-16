import { CallModel } from "./CallModel.js";
import { PatientModel } from "./PatientModel.js";
import { PersonModel } from "./PersonModel.js";

export class FillCallRequestModel {
    constructor({
        callDto = {},
        patientDto = {},
        personCreateRequest = {}
    } = {}) {
        this.callDto = new CallModel(callDto);
        this.patientDto = new PatientModel(patientDto);
        this.personCreateRequest = new PersonModel(personCreateRequest);
    }
}