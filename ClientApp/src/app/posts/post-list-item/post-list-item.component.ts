import { PostsService } from './../posts.service';
import { Component, Input } from '@angular/core';
import { Post } from '../post.model';

@Component({
    selector: 'post-list-item',
    templateUrl: 'post-list-item.component.html',
    styleUrls: ['./post-list-item.component.scss']
})
export class PostListItemComponent {

    @Input() post: Post;

    constructor(public postsService: PostsService) {

    }
}
