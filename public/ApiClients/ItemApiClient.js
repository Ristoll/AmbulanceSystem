export class ItemApiClient {
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

    // --- Item API Methods ---

    createItem(itemDto) {
        return this.request("api/item/create-item", "POST", itemDto);
    }

    deleteItem(itemId) {
        return this.request(`api/item/delete-item/${itemId}`, "DELETE");
    }

    searchItem(itemId) {
        return this.request(`api/item/search-item?itemId=${itemId}`);
    }

    loadItems() {
        return this.request("api/item/load-items");
    }

    assignItemToBrigade(brigadeItemDto) {
        return this.request("api/item/assign-item-to-brigade", "POST", brigadeItemDto);
    }

    unassignItemFromBrigade(brigadeItemId) {
        return this.request(`api/item/unassign-item-from-brigade/${brigadeItemId}`, "DELETE");
    }

    loadBrigadeItems() {
        return this.request("api/item/load-brigade-items");
    }

    increaseBrigadeItemQuantity(itemId, amount) {
        return this.request(`api/item/increase-brigade-item-quantity?itemId=${itemId}&amount=${amount}`, "POST");
    }

    decreaseBrigadeItemQuantity(itemId, amount) {
        return this.request(`api/item/decrease-brigade-item-quantity?itemId=${itemId}&amount=${amount}`, "POST");
    }
}
