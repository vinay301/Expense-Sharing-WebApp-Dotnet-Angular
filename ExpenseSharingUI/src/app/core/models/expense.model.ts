import { User } from "./user.model";


export interface Expense {
    expenseSplits: any;
    showDetails: boolean;
    id:string,
    date:Date,
    amount:number,
    description:string,
    groupId:string
    paidByUserId : string, //payerId -> one who pays the expense
    splitWithUserIds : string[], //payeeId's -> among which the expense is splitted
    user : User //This is used at the time of expenseSplit to fetch the details of splitted Users
}