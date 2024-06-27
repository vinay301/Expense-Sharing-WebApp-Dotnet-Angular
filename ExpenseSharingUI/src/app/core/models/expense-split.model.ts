import { Expense } from "./expense.model";
import { User } from "./user.model";

export interface ExpenseSplit{
    id: string;
    user : User[];
    amount: number;
    expenseSplits: Expense[];
}