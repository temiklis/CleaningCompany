export interface OrderRequestWithProducts {
  Id: number;
  UserEmail: string;
  UserFIO: string;
  UserAddress: string;
  Products: string;
  RenderedDate: Date;
  RequestedDate: Date;
}
