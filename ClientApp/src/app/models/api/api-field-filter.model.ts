export class APIFieldFilter {

    constructor(public fieldName: string, public filterAction: FilterAction, public fieldValue: string) {

    }
}

export enum FilterAction {
    Equals,
    Distinct,
    Contains,
    LessThan,
    LessOrEqualThan,
    GreaterThan,
    GreaterOrEqualThan
}