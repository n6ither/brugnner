import { Component, OnInit } from '@angular/core';
import { Post } from '../../posts/post.model';
import { PostsService } from '../../posts/posts.service';
import { NbDialogService, NbToastrService, NbGlobalPhysicalPosition } from '@nebular/theme';
import { ConfirmComponent } from '../../confirm/confirm.component';

@Component({
    selector: 'admin-posts-list',
    templateUrl: './admin-posts-list.component.html',
    styleUrls: ['./admin-posts-list.component.scss']
})
export class AdminPostsListComponent implements OnInit {

    posts: Post[];
    isLoading: boolean;
    searchTerm: string;

    constructor(private postService: PostsService,
        private toastrService: NbToastrService,
        private dialogService: NbDialogService) {

    }

    ngOnInit(): void {
        this.loadPosts();
    }

    search(): void {
        this.isLoading = true;

        this.postService.getPostsAsAdmin(this.searchTerm).subscribe(response => {
            this.posts = response.result.items;
            this.isLoading = false;
        });
    }

    togglePostPublished(id: string): void {
        this.postService.togglePostPublished(id).subscribe(response => {

            let postIndex = this.posts.findIndex(item => item.id == response.result.id);
            this.posts[postIndex] = response.result;

            const message = 'Post ' + ((response.result.isPublished) ? 'published' : 'unpublished');
            this.toastrService.success(message, 'Brugnner', { position: NbGlobalPhysicalPosition.BOTTOM_RIGHT });
        });
    }

    deletePost(id: string): void {
        this.dialogService.open(ConfirmComponent, {
            context: { title: 'Delete post', text: 'Are you sure?' }
        }).onClose.subscribe(result => {

            if (result) {
                this.postService.deletePost(id).subscribe(response => {

                    const message = "Post deleted successfully";
                    this.toastrService.success(message, 'Brugnner', { position: NbGlobalPhysicalPosition.BOTTOM_RIGHT });

                    this.loadPosts();
                });
            }
        });
    }

    private loadPosts() {
        this.isLoading = true;

        this.postService.getPostsAsAdmin().subscribe(response => {
            this.posts = response.result.items;
            this.isLoading = false;
        });
    }
}