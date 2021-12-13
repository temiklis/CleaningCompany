export interface OrderRequestDetails {
  Id: number;
  UserFIO: string;
  UserEmail: string;
  UserAddress: string;
  RequestedDate: Date;
  RenderedDate: Date;
  Products: string[];
}
