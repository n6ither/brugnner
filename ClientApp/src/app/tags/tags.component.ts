import { PostsService } from './../posts/posts.service';
import { Tag } from './../posts/tag.model';

import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-tags',
    templateUrl: './tags.component.html',
    styleUrls: ['./tags.component.scss']
})
export class TagsComponent implements OnInit {

    tags: Tag[] = [];

    constructor(private postsService: PostsService) {

    }

    ngOnInit(): void {
        this.postsService.getTags().subscribe(response => {
            this.tags = response.result;
        });
    }
}
