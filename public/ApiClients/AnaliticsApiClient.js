export class AnaliticsApiClient {
    constructor(baseUrl, tokenProvider) {
        this.baseUrl = baseUrl;
        this.tokenProvider = tokenProvider;
    }

    async request(url, method = "GET", body = null) {
        const token = await this.tokenProvider();
        const options = {
            method: method,
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            }
        };

        if (body !== null) {
            options.body = JSON.stringify(body);
        }

        const response = await fetch(`${this.baseUrl}/${url}`, options);

        if (!response.ok) {
            const errorText = await response.text();
            alert(errorText);
            return null;
        }

        if (response.status === 204) return true;

        return await response.json();
    }

    // --- API METHODS ---

    /**
     * GET api/analitics/brigade-recources?from=yyyy-MM-dd&to=yyyy-MM-dd
     */
    async getBrigadeResourceAnalitics(from, to) {
        const fromStr = encodeURIComponent(from.toISOString());
        const toStr = encodeURIComponent(to.toISOString());
        return await this.request(`api/analitics/brigade-recources?from=${fromStr}&to=${toStr}`);
    }

    /**
     * GET api/analitics/calls?from=yyyy-MM-dd&to=yyyy-MM-dd
     */
    async getCallAnalitics(from, to) {
        const fromStr = encodeURIComponent(from.toISOString());
        const toStr = encodeURIComponent(to.toISOString());
        return await this.request(`api/analitics/calls?from=${fromStr}&to=${toStr}`);
    }

    /**
     * GET api/analitics/deceases
     */
    async getDeceaseAnalitics() {
        return await this.request("api/analitics/deceases");
    }

    /**
     * GET api/analitics/full?from=yyyy-MM-dd&to=yyyy-MM-dd
     */
    async getFullAnalitics(from, to) {
        const fromStr = encodeURIComponent(from.toISOString());
        const toStr = encodeURIComponent(to.toISOString());
        return await this.request(`api/analitics/full?from=${fromStr}&to=${toStr}`);
    }
}
