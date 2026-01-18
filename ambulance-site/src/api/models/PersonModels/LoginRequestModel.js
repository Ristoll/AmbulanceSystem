export class LoginRequestModel {
    constructor(Login, Password) {
        console.log('Authenticate called with:', { Login, Password });
        this.Login = Login;
        this.Password = Password;
    }
}