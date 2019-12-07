import { environment } from './../../environments/environment';
import { IdPlaceholder } from './../models/api/api-id-placeholder.model';
import { APIListParams } from '../models/api/api-list-params.model';
import { APIListResult } from '../models/api/api-list-result.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Post } from './post.model';
import { APIResponse } from '../models/api/api-response.model';
import { delay } from 'rxjs/operators';
import { Tag } from './tag.model';
import { DateTimePipe } from '../pipes/datetime.pipe';
const readingTime = require('reading-time');

@Injectable()
export class PostsService {

    constructor(private http: HttpClient, private dateTimePipe: DateTimePipe) {

    }

    getPosts(page: number = 0, searchTerm?: string): Observable<APIResponse<APIListResult<Post[]>>> {
        const take = environment.defaultPageSize;
        const skip = (page - 1) * take;
        let url = `posts?OrderByField=CreatedAt&OrderByDirection=desc&Skip=${skip}&Take=${take}`;

        if (searchTerm) {
            url += `&Filters=Title.Contains("${searchTerm}") OR Excerpt.Contains("${searchTerm}") OR Content.Contains("${searchTerm}") OR Tags.Any(x => x.Name.Equals("${searchTerm}"))`;
        }

        return this.http.get<APIResponse<APIListResult<Post[]>>>(url)
            .pipe(delay(1000));
    }

    getPostsByTag(tag: string, page: number = 0): Observable<APIResponse<APIListResult<Post[]>>> {
        const take = environment.defaultPageSize;
        const skip = (page - 1) * take;

        return this.http.get<APIResponse<APIListResult<Post[]>>>(`posts/tag/${tag}?OrderByField=CreatedAt&OrderByDirection=asc&Skip=${skip}&Take=${take}`)
            .pipe(delay(1000));
    }

    getPostBySlug(slug: string): Observable<APIResponse<Post>> {
        return this.http.get<APIResponse<Post>>('posts/slug/' + slug);
    }

    getRandomPost(): Observable<APIResponse<Post>> {
        return this.http.get<APIResponse<Post>>('posts/random');
    }

    getLatestPost(): Observable<APIResponse<Post>> {
        return this.http.get<APIResponse<Post>>('posts/latest');
    }

    getTags(): Observable<APIResponse<Tag[]>> {
        return this.http.get<APIResponse<Tag[]>>('posts/tags');
    }

    getPostsAsAdmin(searchTerm?: string): Observable<APIResponse<APIListResult<Post[]>>> {

        let url = 'posts/full';

        if (searchTerm) {
            url += `?Filters=Title.Contains("${searchTerm}") OR Excerpt.Contains("${searchTerm}") OR Content.Contains("${searchTerm}") OR Tags.Any(x => x.Name.Equals("${searchTerm}"))`;
        }

        return this.http.get<APIResponse<APIListResult<Post[]>>>(url)
            .pipe(delay(1000));
    }

    getPostById(id: number): Observable<APIResponse<Post>> {
        return this.http.get<APIResponse<Post>>('posts/' + id);
    }

    editPost(post: Post): Observable<APIResponse<Post>> {
        if (post.id) {
            return this.updatePost(post);
        }
        else {
            return this.createPost(post);
        }
    }

    togglePostPublished(id: string): Observable<APIResponse<Post>> {
        const resource = new IdPlaceholder<string>(id);
        return this.http.put<APIResponse<Post>>('posts/togglepublication', resource);
    }

    deletePost(id: string): Observable<APIResponse<string>> {
        return this.http.delete<APIResponse<string>>('posts/' + id);
    }

    postDatesAndAuthor(post: Post): string {
        let message = this.dateTimePipe.transform(post.createdAt);
        message += " by " + environment.author;

        if (post.updatedAt) {
            message += " (Last edition: " + this.dateTimePipe.transform(post.updatedAt) + ")";
        }

        return message;
    }

    readingTime(post: Post): string {
        return readingTime(post.content).text;
    }

    private createPost(post: Post): Observable<APIResponse<Post>> {
        return this.http.post<APIResponse<Post>>('posts', post);
    }

    private updatePost(post: Post): Observable<APIResponse<Post>> {
        return this.http.put<APIResponse<Post>>('posts', post);
    }
}
