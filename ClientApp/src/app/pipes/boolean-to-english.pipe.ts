import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'booleanToEnglish' })
export class BooleanToEnglishPipe implements PipeTransform {

    transform(value: boolean, capitalize: boolean): string {

        if (capitalize)
            return value ? 'Yes' : 'No';

        return value ? 'yes' : 'no';
    }
}
