import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({ name: 'datetime' })
export class DateTimePipe implements PipeTransform {

    transform(date: Date): string {

        return moment(date).format("MMM Do[,] YYYY [at] HH:mm")
    }
}
