import { IPagedResult } from './IPagedResult';
import { IPagedRequest } from './IPagedRequest';

export class PagedRequest implements IPagedRequest {
  public pageIndex: number = 1;
  public pageSize: number = 10;

  constructor(current: number, size: number) {
    this.pageIndex = current;
    this.pageSize = size;
  }

}
