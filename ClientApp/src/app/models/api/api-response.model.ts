import { APIError } from './api-error.model';

export class APIResponse<T> {
    statusCode: number;
    description: string;
    error: APIError;
    result: T;
}