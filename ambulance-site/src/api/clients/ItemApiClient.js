import { AuthService } from "../services/AuthService.js";
import { ApiConfig } from "../services/ApiConfig.js";

export default class ItemApiClient {
    constructor(baseUrl = ApiConfig.getBaseUrl()) {
        this.baseUrl = baseUrl;
    }

    async request(url, method = "GET", body = null) {
        const headers = AuthService.addBearerHeader({ "Content-Type": "application/json" });
        const options = { method, headers };
        if (body) {
            options.body = JSON.stringify(body);
            console.log(body);
        }

        const response = await fetch(`${this.baseUrl}/api/item/${url}`, options);

        if (response.status === 401) {
            AuthService.deleteUserData();
            window.location.href = "/start";
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

    // --- Item API Methods ---

    createItem(itemDto) {
        return this.request("create-item", "POST", itemDto);
    }

    deleteItem(itemId) {
        return this.request(`delete-item/${itemId}`, "DELETE");
    }

    searchItem(itemId) {
        return this.request(`search-item?itemId=${itemId}`);
    }

    loadItems() {
        return this.request("load-items");
    }

    assignItemToBrigade(brigadeItemDto) {
        return this.request("assign-item-to-brigade", "POST", brigadeItemDto);
    }

    unassignItemFromBrigade(brigadeItemId) {
        return this.request(`unassign-item-from-brigade/${brigadeItemId}`, "DELETE");
    }

    loadBrigadeItems(brigadeId) {
        return this.request(`load-brigade-items?brigadeId=${brigadeId}`, "GET");
    }

    increaseBrigadeItemQuantity(itemId, amount) {
        return this.request(`increase-brigade-item-quantity?itemId=${itemId}&amount=${amount}`, "POST");
    }

    decreaseBrigadeItemQuantity(itemId, amount) {
        return this.request(`decrease-brigade-item-quantity?itemId=${itemId}&amount=${amount}`, "POST");
    }
}
