export interface IUser {
  id: number;
  name: string;
  surname: string;
  birthDate: Date;
  emailAddress: string;
  isActive: boolean;
  userTypeId: number;
  userTitleId: number;
}
