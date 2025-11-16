export class CallApiClient {
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

    /** POST api/call/create-call */
    async createCall(callDto) {
        return await this.request("api/call/create-call", "POST", callDto);
    }

    /** DELETE api/call/delete-call/{callId} */
    async deleteCall(callId) {
        return await this.request(`api/call/delete-call/${callId}`, "DELETE");
    }

    /** POST api/call/fill-call */
    async fillCall(fillCallRequest) {
        return await this.request("api/call/fill-call", "POST", fillCallRequest);
    }

    /** GET api/call/load-call?callId=... */
    async loadCall(callId) {
        return await this.request(`api/call/load-call?callId=${callId}`);
    }

    /** GET api/call/load-calls */
    async loadCalls() {
        return await this.request("api/call/load-calls");
    }

    /** GET api/call/search-person?param=value&... */
    async searchPerson(personProfileDTO) {
        const query = new URLSearchParams(personProfileDTO).toString();
        return await this.request(`api/call/search-person?${query}`);
    }
}
