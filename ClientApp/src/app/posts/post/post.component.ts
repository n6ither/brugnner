import { Location } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NbDialogService, NbGlobalPhysicalPosition, NbToastrService } from '@nebular/theme';

import { ConfirmComponent } from '../../confirm/confirm.component';
import { PostsService } from '../posts.service';
import { AuthService } from './../../auth/auth.service';
import { Post } from './../post.model';

@Component({
    selector: 'post',
    templateUrl: 'post.component.html',
    styleUrls: ['./post.component.scss']
})
export class PostComponent {

    post: Post;
    isLoading: boolean = true;
    togglePublicationButtonText: string = 'Publish';

    constructor(private route: ActivatedRoute,
        private toastrService: NbToastrService, private dialogService: NbDialogService,
        private router: Router, public postsService: PostsService, private location: Location,
        public authService: AuthService) {

        route.params.subscribe(val => {
            const lastSegment = this.route.snapshot.url[this.route.snapshot.url.length - 1];

            if (lastSegment.path === 'random') {
                this.loadRandomPost();
            } else if (lastSegment.path === 'latest') {
                this.loadLatestPost();
            }
            else {
                this.loadPostBySlug();
            }
        });
    }

    togglePostPublished(id: string): void {
        this.postsService.togglePostPublished(id).subscribe(response => {
            this.togglePublicationButtonText = (response.result.isPublished) ? 'Unpublish' : 'Publish';
            const message = 'Post ' + ((response.result.isPublished) ? 'published' : 'unpublished');

            this.toastrService.success(message, 'Brugnner', { position: NbGlobalPhysicalPosition.BOTTOM_RIGHT });
        });
    }

    deletePost(id: string): void {
        this.dialogService.open(ConfirmComponent, {
            context: { title: 'Delete post', text: 'Are you sure?' }
        }).onClose.subscribe(result => {

            if (result) {
                this.postsService.deletePost(id).subscribe(response => {
                    const message = "Post deleted successfully";
                    this.toastrService.success(message, 'Brugnner', { position: NbGlobalPhysicalPosition.BOTTOM_RIGHT });

                    this.router.navigate(['/posts']);
                });
            }
        });
    }

    private loadPostBySlug() {
        const slug = this.route.snapshot.params['slug'];

        this.postsService.getPostBySlug(slug).subscribe(response => {
            this.showPost(response.result);
        });
    }

    private loadRandomPost() {
        this.postsService.getRandomPost().subscribe(response => {
            this.showPost(response.result);
        });
    }

    private loadLatestPost() {
        this.postsService.getLatestPost().subscribe(response => {
            this.showPost(response.result);
        });
    }

    private showPost(post: Post) {
        this.post = post;
        this.isLoading = false;
        this.togglePublicationButtonText = (this.post.isPublished) ? 'Unpublish' : 'Publish';

        this.location.replaceState('posts/' + this.post.slug);
    }
}
