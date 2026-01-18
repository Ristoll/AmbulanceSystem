import { MedicalCardModel } from "./MedicalCardModel.js";
import { CallModel } from "./CallModel.js";

export class PatientModel {
    constructor({
        personId = -1,
        allergies = [],
        chronicDiseases = [],
        calls = [],
        medicalCard = null,
    } = {}) {
        this.personId = personId;
        this.allergies = allergies; // залишаємо як є, бо це просто масив рядків
        this.chronicDiseases = chronicDiseases; // залишаємо як є, бо це просто масив рядків
        this.calls = calls.map(r => new CallModel(r));
        this.medicalCard = medicalCard ? new MedicalCardModel(medicalCard) : null;
    }
}