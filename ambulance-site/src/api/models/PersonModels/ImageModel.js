export class ImageModel {
    constructor({ bytes = null, contentType = null } = {}) {
        this.bytes = bytes;
        this.contentType = contentType;
    }

    toDataUrl() {
        if (!this.bytes || !this.contentType) return null;

        const mime = this.contentType === ".png"
            ? "image/png"
            : "image/jpeg";

        return `data:${mime};base64,${this.bytes}`;
    }

    static async fromFile(file) {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.onload = () => {
                const base64 = reader.result.split(',')[1]; // отримуємо тільки base64 без префіксу
                resolve(new ImageModel({
                    bytes: base64,
                    contentType: `.${file.name.split('.').pop().toLowerCase()}`
                }));
            };
            reader.onerror = reject;
            reader.readAsDataURL(file);
        });
    }
}
