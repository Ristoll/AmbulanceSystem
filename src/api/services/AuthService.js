export class AuthService {
    static deleteToken(token){
        localStorage.removeItem("jwtToken");
    }

    static saveToken(token){
        console.log(token); // тимчасова демонстрація
        localStorage.setItem("jwtToken", token);
    }

    static getToken() {
        return localStorage.getItem("jwtToken");
    }

    // зберігаємо роль та логін користувача теж в браузері
    static saveUserInfo({ login, userRole }) {
        localStorage.setItem("userLogin", login);
        localStorage.setItem("userRole", userRole);
    }

    static getUserInfo() {
        return {
            login: localStorage.getItem("userLogin"),
            userRole: localStorage.getItem("userRole"),
        };
    }

    static addBearerHeader(headers = {}) {
        const token = this.getToken();
        if (token) {
            return { ...headers, Authorization: `Bearer ${token}` }; // розпакували оператором всі хедери які є та додали свій
        }

        return headers;
    }
}
