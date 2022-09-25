export default class Envelope{
    constructor(username, message, date = Date.now()){
        this.username = username;
        this.message = message;
        this.date = date;
    }
}