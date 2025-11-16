import { PersonProfileModel } from "./PersonProfileModel.js";
import { CallModel } from "../AnaliticsModels/CallModel.js"; // або де у тебе зберігаються Call моделі
import { MedicalCardModel } from "./MedicalCardModel.js"; // створи відповідну модель

export class PatientModel extends PersonProfileModel {
    constructor({
        personId = 0,
        allergies = [],
        chronicDiseases = [],
        calls = [],
        medicalCards = [],
        ...personProfile
    } = {}) {
        super(personProfile);
        this.personId = personId;
        this.allergies = allergies;
        this.chronicDiseases = chronicDiseases;
        this.calls = calls.map(c => new CallModel(c));
        this.medicalCards = medicalCards.map(m => new MedicalCardModel(m));
    }
}