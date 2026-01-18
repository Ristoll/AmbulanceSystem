export class MedicalRecordModel {
    constructor({
        medicalRecordId = 0,
        medicalCardId = 0,
        brigadeMemberId = 0,
        dataTime = null,
        diagnoses = null,
        symptoms = null,
        treatment = null,
        imageUrl = null,
        brigadeMemberName = null,
        medicalCardPatientName = null
    } = {}) {
        this.medicalRecordId = medicalRecordId;
        this.medicalCardId = medicalCardId;
        this.brigadeMemberId = brigadeMemberId;
        this.dataTime = dataTime ? new Date(dataTime) : null;
        this.diagnoses = diagnoses;
        this.symptoms = symptoms;
        this.treatment = treatment;
        this.imageUrl = imageUrl;
        this.brigadeMemberName = brigadeMemberName;
        this.medicalCardPatientName = medicalCardPatientName;
    }
}