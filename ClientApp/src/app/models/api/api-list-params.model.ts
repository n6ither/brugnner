import { APIFieldFilter } from './api-field-filter.model';

export class APIListParams {
    orderByField: string;
    orderByDirection: string;
    searchTerm: string;
    skip: number;
    take: number;
    fieldFilters: APIFieldFilter[];
}