import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Post } from '../posts/post.model';
import { PostsService } from './../posts/posts.service';

@Component({
    selector: 'app-search',
    templateUrl: 'search.component.html',
    styleUrls: ['./search.component.scss']
})

export class SearchComponent implements OnInit {

    posts: Post[] = [];
    page: number = 1;
    loadMoreButtonText: string = 'Load more..';

    constructor(public postsService: PostsService, private route: ActivatedRoute) {

    }

    ngOnInit(): void {
        this.loadPosts();
    }

    loadPosts(): void {

        this.loadMoreButtonText = 'Loading..';


        this.postsService.getPosts(this.page, this.route.snapshot.queryParams["filter"])
            .subscribe(response => {
                this.posts.push(...response.result.items);
                this.loadMoreButtonText = 'Load more..';
                this.page++;
            });
    }
}