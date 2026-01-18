import { AuthService } from "../services/AuthService.js";
import { ApiConfig } from "../services/ApiConfig.js";

export class PersonApiClient {
    constructor(baseUrl = ApiConfig.getBaseUrl()) {
        this.baseUrl = baseUrl;
    }

    async request(url, method = "GET", body = null) {
        const headers = AuthService.addBearerHeader({ "Content-Type": "application/json" });
        const options = { method, headers };
        if (body) {
            options.body = JSON.stringify(body);
            console.log(body); // для відладки
        }

        const response = await fetch(`${this.baseUrl}/api/person/${url}`, options);

        if (response.status === 401) {
            AuthService.deleteUserData();
            window.location.href = "/start"; // частинка від реакту, щоб переводило назад до головної сторінки при невалідності токену
            return null;
        }

        if (!response.ok) {
            const text = await response.text();
            throw new Error(text);
        }

        const text = await response.text();
        if (!text) return true;

        return JSON.parse(text);
    }

    // === AUTH ===
    async authenticate(LoginRequestModel) {
        return await this.request("authetificate", "POST", LoginRequestModel);
    }

    async changePassword(ChangePasswordRequestModel) {
        return await this.request("change-password", "POST", ChangePasswordRequestModel);
    }

    async adminResetPassword(targetPersonId, newPassword) {
        return await this.request(`admin-reset-password/${targetPersonId}`, "POST", newPassword);
    }

    // === PERSON CRUD ===
    async createPerson(personData) {
        return await this.request("create", "POST", personData);
    }

    async createPatient(personData) {
        return await this.request("create-patient", "POST", personData);
    }

    async updatePerson(updateData) {
        return await this.request("update", "PUT", updateData);
    }

    async deletePerson(personId) {
        return await this.request(`delete/${personId}`, "DELETE");
    }

    async getPersonProfile(personId) {
        return await this.request(`profile/${personId}`, "GET");
    }

    async getPatientData(personId) {
        return await this.request(`patientData/${personId}`, "GET");
    }
 
    async loadPersons() {
        return await this.request("load-persons", "GET");
    }

    async loadPersonRoles() {
        return await this.request("roles", "GET");
    }
}
