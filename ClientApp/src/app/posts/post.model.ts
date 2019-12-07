import { Tag } from './tag.model';

export class Post {
    id: string;
    title: string;
    excerpt: string;
    slug: string;
    content: string;
    tags: Tag[];
    createdAt: Date;
    updatedAt: Date;
    isPublished: boolean;
    previousPostTitle: string;
    previousPostSlug: string;
    nextPostTitle: string;
    nextPostSlug: string;
}