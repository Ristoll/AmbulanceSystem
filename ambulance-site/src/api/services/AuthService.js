export class AuthService {
    static saveToken(token){
        console.log(token); // тимчасова демонстрація
        localStorage.setItem("jwtToken", token);
    }

    static getToken() {
        return localStorage.getItem("jwtToken");
    }

    static getPersonId() {
        return localStorage.getItem("personId");
    }

    // зберігаємо роль та логін користувача теж в браузері
    static saveUserInfo(login, userRole, personId) {
        console.log(login, userRole); 
        localStorage.setItem("userLogin", login);
        localStorage.setItem("userRole", userRole);
        localStorage.setItem("personId", personId);
    }

     // очищаємо всі дані користувача з браузера
    static deleteUserData() {
        localStorage.removeItem("jwtToken");
        localStorage.removeItem("userLogin");
        localStorage.removeItem("userRole");
        localStorage.removeItem("personId");
    }

    static getUserInfo() {
        return {
            login: localStorage.getItem("userLogin"),
            userRole: localStorage.getItem("userRole"),
            personId: localStorage.getItem("personId")
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
