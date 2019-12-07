import { CKEditorModule } from 'ng2-ckeditor';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { NbButtonModule, NbCardModule, NbCheckboxModule, NbIconModule, NbInputModule, NbListModule } from '@nebular/theme';


import { BooleanToEnglishPipe } from '../pipes/boolean-to-english.pipe';
import { EditPostComponent } from './edit-post/edit-post.component';
import { DateTimePipe } from './../pipes/datetime.pipe';
import { SpinnerCardModule } from './../spinner-card/spinner-card.module';
import { AdminPostsListComponent } from './admin-posts-list/admin-posts-list.component';
import { AdminComponent } from './admin.component';

const routes: Routes = [
    { path: '', component: AdminComponent },
    { path: 'posts/edit/:id', component: EditPostComponent },
    { path: 'posts/new', component: EditPostComponent }
]

@NgModule({
    imports: [
        RouterModule.forChild(routes),
        CommonModule,
        NbCardModule,
        NbButtonModule,
        NbListModule,
        NbInputModule,
        NbButtonModule,
        FormsModule,
        ReactiveFormsModule,
        CKEditorModule,
        NbIconModule,
        NbCheckboxModule,
        SpinnerCardModule,
    ],
    exports: [

    ],
    declarations: [
        AdminComponent,
        AdminPostsListComponent,
        EditPostComponent,
        DateTimePipe,
        BooleanToEnglishPipe
    ],
    providers: [
        DateTimePipe,
        BooleanToEnglishPipe
    ]
})
export class AdminModule {

}