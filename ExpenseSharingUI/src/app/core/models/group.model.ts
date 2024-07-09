import { Expense } from "./expense.model";
import { UserGroup } from "./user-group.model";
import { User } from "./user.model"

export interface Group{
 id : string,
 name : string,
 description : string   
 createdDate : Date,
 memberIds: string[];
 userGroups : UserGroup[];
 expenses : Expense[]
 adminIds:string[],
 admins: User[]
}