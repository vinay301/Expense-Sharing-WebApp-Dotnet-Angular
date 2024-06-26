

export interface Expense {
    id:string,
    date:Date,
    amount:number,
    description:string,
    groupId:string
    paidByUserId : string, //payerId -> one who pays the expense
    splitWithUserIds : string[], //payeeId's -> among which the expense is splitted
}