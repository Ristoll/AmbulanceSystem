export class BrigadeApiClient {
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

        // Обробка помилки (аналог HandleErrorAsync)
        if (!response.ok) {
            const errorText = await response.text();
            alert(errorText);
            return null;
        }

        // Якщо не треба тіло → просто повертаємо true
        if (response.status === 204) {
            return true;
        }

        return await response.json();
    }

    // ---------- API METHODS ----------

    async getAllBrigades() {
        return await this.request("api/brigade");
    }

    async getBrigadeById(brigadeId) {
        return await this.request(`api/brigade/${brigadeId}`);
    }

    async getBrigadesByState(stateId) {
        return await this.request(`api/brigade/state/${stateId}`);
    }

    // POST api/brigade/update-brigade-state?brigadeStateId=...
    async updateBrigadeState(brigadeStateId, brigadeDto) {
        return await this.request(
            `api/brigade/update-brigade-state?brigadeStateId=${brigadeStateId}`,
            "POST",
            brigadeDto
        );
    }

    // PUT api/brigade/update-brigade
    async updateBrigade(brigadeDto) {
        return await this.request(
            "api/brigade/update-brigade",
            "PUT",
            brigadeDto
        );
    }
}
