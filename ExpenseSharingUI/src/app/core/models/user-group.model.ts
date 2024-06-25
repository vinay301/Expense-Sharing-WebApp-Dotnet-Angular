import { User } from "./user.model";

export interface UserGroup {
    id: string;
    userId: string;
    user: User;
  }