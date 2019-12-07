export class APIListResult<T> {
    items: T;
    totalItemsCount: number;
    firstPage: number;
    lastPage: number;
    previousPage: number;
    nextPage: number;
}