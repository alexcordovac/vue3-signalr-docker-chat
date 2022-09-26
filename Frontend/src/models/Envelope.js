export default class Envelope{
    constructor(user,  message, groupType, date = Date.now()){
        this.user = user;
        this.groupType = groupType;
        this.message = message;
        this.date = date;
    }
}