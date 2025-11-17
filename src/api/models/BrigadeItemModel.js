export class BrigadeItemModel {
    constructor({
        brigadeItemId = 0,
        brigadeId = 0,
        itemId = 0,
        quantity = null,
        expiryDate = null, // DateOnly у C# -> рядок або Date в JS
        brigadeName = "",
        itemName = "",
        unitType = ""
    } = {}) {
        this.brigadeItemId = brigadeItemId;
        this.brigadeId = brigadeId;
        this.itemId = itemId;
        this.quantity = quantity;
        this.expiryDate = expiryDate ? new Date(expiryDate) : null; // якщо передано, перетворюємо в Date
        this.brigadeName = brigadeName;
        this.itemName = itemName;
        this.unitType = unitType;
    }
}