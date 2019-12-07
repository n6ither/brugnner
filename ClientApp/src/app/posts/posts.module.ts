import { SpinnerCardModule } from './../spinner-card/spinner-card.module';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NbButtonModule, NbCardModule, NbIconModule, NbListModule, NbTooltipModule } from '@nebular/theme';

import { TagsPageComponent } from '../tags/tags-page/tags-page.component';
import { TagsComponent } from '../tags/tags.component';
import { PostListItemComponent } from './post-list-item/post-list-item.component';
import { PostPlaceholderComponent } from './post-placeholder/post-placeholder.component';
import { PostComponent } from './post/post.component';
import { PostsListComponent } from './posts-list/posts-list.component';
import { PostsService } from './posts.service';

const routes: Routes = [
    { path: '', component: PostsListComponent },
    { path: 'posts', component: PostsListComponent },
    { path: ':slug', component: PostComponent },
    { path: 'tags/all', component: TagsPageComponent },
    { path: 'tag/:tag', component: PostsListComponent },
    { path: 'random', component: PostComponent },
    { path: 'lastest', component: PostComponent }
]

@NgModule({
    imports: [
        RouterModule.forChild(routes),
        CommonModule,
        NbListModule,
        NbCardModule,
        NbTooltipModule,
        NbIconModule,
        NbButtonModule,
        SpinnerCardModule
    ],
    exports: [
        RouterModule
    ],
    declarations: [
        PostsListComponent,
        PostListItemComponent,
        PostComponent,
        PostPlaceholderComponent,
        TagsPageComponent,
        TagsComponent
    ],
    providers: [
        PostsService
    ]
})
export class PostsModule {

}