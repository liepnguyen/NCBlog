import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'post-item',
    templateUrl: './post-item.component.html',
    styleUrls: ['./post-item.component.scss']
})
export class PostItemComponent implements OnInit {
    ngOnInit() {
    }
}

interface BlogPost {
    imageUrl: string;
    title: string;
    description: string;
    numOfComments: number;
    numOfLikes: number;
    postedDateTime: string;
}