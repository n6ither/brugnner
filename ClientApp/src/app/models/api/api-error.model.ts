import { APIValidationError } from './api-validation-error.model';

export class APIError {
    message: string;
    details: string;
    validationErrors: APIValidationError[];
}