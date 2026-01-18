import { ApiConfig } from "../services/ApiConfig.js";
import { AuthService } from "../services/AuthService.js";

export class CallApiClient {
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
   
           const response = await fetch(`${this.baseUrl}/api/call/${url}`, options);
   
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

    /** POST api/call/create-and-fill-call - створення порожнього дзвінка, повертає callId */
    async createAndFillCall(call, person = null) {
    const payload = {
        Call: call,
        Person: person
    };
    return await this.request("create-and-fill-call", "POST", payload);
}

    /** DELETE api/call/delete-call/{callId} */
    async deleteCall(callId) {
        await this.request(`delete-call/${callId}`, "DELETE");
    }

    /** GET api/call/load-call?callId=... */
    async loadCall(callId) {
        return await this.request(`load-call?callId=${callId}`);
    }
  
    /** GET api/call/load-calls */
    async loadCalls() {
        return await this.request("load-calls");
    }

    /** GET api/call/load-hospitals */
    async loadHospitals() {
        return await this.request("load-hospitals");
    }

    /** GET api/call/search-person/param=value&...  + замінює лоад пацієнтів*/
    async SearchPatient(text) {
        return await this.request(`search-person/${text}`);
    }
}
