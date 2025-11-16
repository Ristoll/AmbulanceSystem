import { MedicalRecordModel } from "./MedicalRecordModel.js";

export class MedicalCardModel {
    constructor({
        medicalCardId = 0,
        patientId = 0,
        creationDate = null,
        bloodType = null,
        height = null,
        weight = null,
        notes = null,
        allergies = [],
        chronicDiseases = [],
        medicalRecords = []
    } = {}) {
        this.medicalCardId = medicalCardId;
        this.patientId = patientId;
        this.creationDate = creationDate ? new Date(creationDate) : null;
        this.bloodType = bloodType;
        this.height = height;
        this.weight = weight;
        this.notes = notes;
        this.allergies = allergies;
        this.chronicDiseases = chronicDiseases;
        this.medicalRecords = medicalRecords.map(r => new MedicalRecordModel(r));
    }
}