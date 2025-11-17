export class MedicalCardApiClient {
    constructor(baseUrl, tokenProvider) {
        this.baseUrl = baseUrl;
        this.tokenProvider = tokenProvider;
    }

    async request(url, method = "GET", body = null) {
        const token = this.tokenProvider ? await this.tokenProvider() : null;
        const options = { method, headers: { "Content-Type": "application/json" } };
        if (token) options.headers["Authorization"] = `Bearer ${token}`;
        if (body) options.body = JSON.stringify(body);

        const response = await fetch(`${this.baseUrl}/${url}`, options);
        if (!response.ok) {
            const errorText = await response.text();
            alert(errorText);
            return null;
        }
        return response.status === 204 ? true : await response.json();
    }

    // --- MedicalCard API Methods ---

    createMedicalCard(medicalCardDto) {
        return this.request("api/medicalcard/create-medical-card", "POST", medicalCardDto);
    }

    updateMedicalCard(medicalCardDto) {
        return this.request("api/medicalcard/update-medical-card", "POST", medicalCardDto);
    }

    searchMedicalCard(personId) {
        return this.request(`api/medicalcard/search-medical-card/${personId}`);
    }

    createMedicalRecord(medicalRecordDto) {
        return this.request("api/medicalcard/create-medical-record", "POST", medicalRecordDto);
    }
}
