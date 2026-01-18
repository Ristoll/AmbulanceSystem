export class AnaliticsApiClient {
    constructor(baseUrl, tokenProvider) {
        this.baseUrl = baseUrl;
        this.tokenProvider = tokenProvider;
    }

    async request(endpoint, method = "GET", body = null) {
        const token = await this.tokenProvider();
        const options = {
            method,
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            }
        };

        if (body !== null) {
            options.body = JSON.stringify(body);
        }

        const response = await fetch(`${this.baseUrl}/${endpoint}`, options);

        if (!response.ok) {
            const errorText = await response.text();
            console.error(`API Error: ${errorText}`);
            throw new Error(errorText);
        }

        if (response.status === 204) return true;

        return await response.json();
    }

    // --- API METHODS ---

    /**
     * GET api/analitics/brigade-resources
     */
    async getBrigadeResourceAnalytics() {
        return await this.request("api/analitics/brigade-resources");
    }

    /**
     * GET api/analitics/calls
     */
    async getCallAnalytics() {
        return await this.request("api/analitics/calls");
    }

    /**
     * GET api/analitics/deceases
     */
    async getDeceaseAnalytics() {
        return await this.request("api/analitics/deceases");
    }

    /**
     * GET api/analitics/allergies
     */
    async getAllergyAnalytics() {
        return await this.request("api/analitics/allergies");
    }
}
