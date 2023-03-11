
// TODO separate file per model
// Better name them as IUser
export interface user {
  id: number;
  name: string;
  surname: string;
  emailAddress: string;
  title: string;
  type: string;
  birthDate: Date;
  isActive: boolean;
}
export interface newUser {
  name: string,
  surname: string,
  birthDate: Date,
  userTypeId: number,
  userTitleId: number,
  emailAddress: string,
  isActive: boolean
}

export interface updatedUser {
  id: number;
  name: string,
  surname: string,
  birthDate: Date,
  userTypeId: 4,
  userTitleId: 1,
  emailAddress: string,
  isActive: boolean
}
