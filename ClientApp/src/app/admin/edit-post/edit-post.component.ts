import { Tag } from '../../posts/tag.model';
import { Router, ActivatedRoute } from '@angular/router';
import { PostsService } from '../../posts/posts.service';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Post } from '../../posts/post.model';
import { NbToastrService, NbGlobalPhysicalPosition } from '@nebular/theme';
import './ckeditor.loader';
import 'ckeditor';

@Component({
    selector: 'edit-post',
    templateUrl: 'edit-post.component.html',
    styleUrls: ['edit-post.component.scss']
})
export class EditPostComponent {

    ckEditorConfig: any = {
        language: 'en',
        height: '320',
        toolbarGroups: [
            { name: 'document', groups: ['mode', 'document', 'doctools'] },
            { name: 'clipboard', groups: ['clipboard', 'undo'] },
            { name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
            { name: 'forms', groups: ['forms'] },
            '/',
            { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
            { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
            { name: 'links', groups: ['links'] },
            { name: 'insert', groups: ['insert'] },
            '/',
            { name: 'styles', groups: ['styles'] },
            { name: 'colors', groups: ['colors'] },
            { name: 'tools', groups: ['tools'] },
            { name: 'others', groups: ['others'] },
            { name: 'about', groups: ['about'] }
        ]
    };

    postForm: FormGroup;
    isLoading: boolean = true;
    isSubmitting: boolean = false;
    cardTitle: string;
    submitText: string;
    tagButtons: string[] = [];
    slug: string;

    constructor(private postService: PostsService, private formBuilder: FormBuilder,
        private router: Router, private toastrService: NbToastrService,
        private route: ActivatedRoute) {

        route.params.subscribe(val => {
            this.buildPostForm();

            const id = this.route.snapshot.params['id'];

            if (id !== undefined) {
                this.postService.getPostById(id).subscribe(response => {
                    this.cardTitle = 'Edit post';
                    this.submitText = 'Update';
                    this.setFormFromPost(response.result);
                    this.isLoading = false;
                });
            }
            else {
                this.cardTitle = 'New post';
                this.submitText = 'Create';
                this.cleanPostForm();
                this.isLoading = false;
            }
        });
    }

    addTag(): void {
        const newTag = this.tagText.value;
        this.tagText.setValue('');

        if (newTag && this.tagButtons.indexOf(newTag) === -1) {
            this.tagButtons.push(newTag);
        }
    }

    removeTag(event): void {

        const tag = event.currentTarget.attributes['id'].value;
        const index = this.tagButtons.findIndex(x => x === tag);

        this.tagButtons.splice(index, 1);
    }

    submitForm(): void {
        this.isSubmitting = true;
        var post: Post = this.getPostFromForm();

        this.postService.editPost(post).subscribe(response => {

            this.toastrService.success('Success', 'Brugnner', { position: NbGlobalPhysicalPosition.BOTTOM_RIGHT });
            this.router.navigate(["posts", response.result.slug]);
        });
    }

    get id() {
        return this.postForm.get('id');
    }

    get title() {
        return this.postForm.get('title');
    }

    get excerpt() {
        return this.postForm.get('excerpt');
    }

    get content() {
        return this.postForm.get('content');
    }

    get tagText() {
        return this.postForm.get('tagText');
    }

    get isPublished() {
        return this.postForm.get('isPublished');
    }

    private buildPostForm() {
        this.postForm = this.formBuilder.group({
            id: new FormControl(''),
            title: new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(150)]),
            excerpt: new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(200)]),
            content: new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(10000)]),
            tagText: new FormControl('', [Validators.minLength(3), Validators.maxLength(100)]),
            isPublished: new FormControl(true),
        });
    }

    private cleanPostForm(): void {
        this.id.setValue('');
        this.title.setValue('');
        this.excerpt.setValue('');
        this.content.setValue('');
        this.tagText.setValue('');
        this.isPublished.setValue(true);
    }

    private setFormFromPost(post: Post) {
        this.id.setValue(post.id);
        this.title.setValue(post.title);
        this.excerpt.setValue(post.excerpt);
        this.content.setValue(post.content);

        post.tags.forEach(tag => {
            this.tagButtons.push(tag.name);
        });

        this.isPublished.setValue(post.isPublished);
        this.slug = post.slug;
    }

    private getPostFromForm() {
        var post: Post = new Post();
        post.id = this.id.value;
        post.title = this.title.value;
        post.excerpt = this.excerpt.value;
        post.content = this.content.value;
        post.tags = new Array<Tag>();

        this.tagButtons.forEach(x => {
            post.tags.push(new Tag(x));
        });

        post.isPublished = this.isPublished.value;

        return post;
    }
}
