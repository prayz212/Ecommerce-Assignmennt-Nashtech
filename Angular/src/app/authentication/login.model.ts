export class Token {
  constructor(public token: string, public expiration: string) {}
}

export class Login {
  userName: string = "";
  password: string = "";
}
