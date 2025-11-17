export class ItemModel {
    constructor({
        itemId = 0,
        itemTypeId = 0,
        unitType = null,
        name = null,
        imageUrl = null
    } = {}) {
        this.itemId = itemId;
        this.itemTypeId = itemTypeId;
        this.unitType = unitType;
        this.name = name;
        this.imageUrl = imageUrl;
    }
}