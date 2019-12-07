import { Post } from './post.model';

export class PostCardData {
    posts: Post[];
    placeholders: any[];
    loading: boolean;
    pageToLoadNext: number;
}