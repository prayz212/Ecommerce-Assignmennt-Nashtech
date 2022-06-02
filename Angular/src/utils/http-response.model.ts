export interface IHttpResponse {
  status: number;
  statusText?: string;
  ok?: boolean;
  body?: any;
}
