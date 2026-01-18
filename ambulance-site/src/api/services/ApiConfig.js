export class ApiConfig {
  static baseUrl = "http://localhost:7090"; // просто база url за замовченням

  static setBaseUrl(url) {
    this.baseUrl = url;
  }

  static getBaseUrl() {
    return this.baseUrl;
  }
}
