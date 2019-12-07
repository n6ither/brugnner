import { environment } from './../../../environments/environment';
import { APIFieldFilter, FilterAction } from './../../models/api/api-field-filter.model';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { PostCardData } from '../post-card-data.model';
import { PostsService } from '../posts.service';
import { APIListResult } from '../../models/api/api-list-result.model';
import { Post } from '../post.model';
import { APIResponse } from '../../models/api/api-response.model';

@Component({
    selector: 'app-posts-list',
    templateUrl: 'posts-list.component.html'
})
export class PostsListComponent implements OnInit {

    postCardData: PostCardData = {
        posts: [],
        placeholders: [],
        loading: false,
        pageToLoadNext: 1
    };

    tag: string = null;

    constructor(private postService: PostsService, private activatedRoute: ActivatedRoute) {

    }

    ngOnInit(): void {
        this.activatedRoute.url.subscribe(segments => {

            if (segments[1]) {
                this.tag = segments[1].path;
            }

            this.loadNext(this.postCardData);
        })
    }

    loadNext(cardData: PostCardData): void {
        if (cardData.loading) { return; }

        cardData.loading = true;
        cardData.placeholders = new Array(environment.defaultPageSize);

        if (!this.tag) {
            this.postService.getPosts(cardData.pageToLoadNext)
                .subscribe(response => {
                    this.handleListResponse(cardData, response);
                });
        }
        else {
            this.postService.getPostsByTag(this.tag, cardData.pageToLoadNext)
                .subscribe(response => {
                    this.handleListResponse(cardData, response);
                });
        }
    }

    private handleListResponse(cardData: PostCardData, response: APIResponse<APIListResult<Post[]>>) {
        cardData.posts.push(...response.result.items);
        cardData.placeholders = [];
        cardData.loading = false;
        cardData.pageToLoadNext++;
    }
}
