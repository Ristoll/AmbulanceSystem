import { ApiConfig } from "../services/ApiConfig.js";
import { AuthService } from "../services/AuthService.js";

export class BrigadeApiClient {
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
   
           const response = await fetch(`${this.baseUrl}/api/brigade/${url}`, options);
   
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

    // ---------- API METHODS ----------

    async getAllBrigades() {
    return await this.request("");
}

    async getBrigadeById(brigadeId) {
        return await this.request(`${brigadeId}`);
    }

    async getBrigadesByState(stateName) {
        return await this.request(`state/${stateName}`);
    }

 
async updateBrigadeState(stateName, brigadeId) {
    return await this.request(
        `update-brigade-state/${brigadeId}`,
        "POST",
        stateName
    );
}

    // PUT api/brigade/update-brigade
    async updateBrigade(brigadeDto) {
        return await this.request(
            "update-brigade",
            "PUT",
            brigadeDto
        );
    }
}
