export interface IPagedResult<T> {
  pageIndex: number;
  pageSize: number;
  hasNextPage: boolean;
  pageCount: number;
  totalCount: number;
  value: Array<T>;
}
