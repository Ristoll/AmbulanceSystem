export class HospitalModel {
  constructor({ hospitalId, name, address, note, brigades = [] } = {}) {
    this.id = hospitalId ?? null;
    this.name = name ?? '';
    this.address = address ?? '';
    this.note = note ?? '';
    this.brigades = brigades ?? [];
  }
}