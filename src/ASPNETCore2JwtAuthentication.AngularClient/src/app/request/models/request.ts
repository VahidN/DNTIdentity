export interface Request {
  id: number;
  processId: number;
  title: string;
  dateRequested: string;
  userId: number;
  currentStateId: number;
  body:string;
}
