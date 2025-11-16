export class PersonApiClient {
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

    // --- Person API Methods ---

    createPerson(request) {
        return this.request("api/person/create", "POST", request);
    }

    createPatient(request) {
        return this.request("api/person/create-patient", "POST", request);
    }

    authetificate(loginRequest) {
        return this.request("api/person/authetificate", "POST", loginRequest);
    }
}
