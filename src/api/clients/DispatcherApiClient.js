export class DispatcherApiClient {
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

    /** GET api/dispatcher/geocode?address=... */
    async geocode(address) {
        const query = new URLSearchParams({ address }).toString();
        return await this.request(`api/dispatcher/geocode?${query}`);
    }

    /** GET api/dispatcher/best-brigades?brigadeTypeId=...&lat=...&lon=...&count=... */
    async getBestBrigades({ brigadeTypeId, lat, lon, count = 1 }) {
        const query = new URLSearchParams({ brigadeTypeId, lat, lon, count }).toString();
        return await this.request(`api/dispatcher/best-brigades?${query}`);
    }
}
