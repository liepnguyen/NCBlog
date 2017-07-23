import { Component, OnInit } from '@angular/core';
declare var $: any;

@Component({
    selector: 'archieves-widget',
    templateUrl: './archieves-widget.component.html',
    styleUrls: ['./archieves-widget.component.css', '../widget.component.css']
})
export class ArchievesWidgetComponent implements OnInit {
    toggleAccordion($event) {
        var ele = $event.srcElement;
        var panel = ele.nextElementSibling;
        if (panel.style.maxHeight) {
            panel.style.maxHeight = null;
        } else {
            panel.style.maxHeight = panel.scrollHeight + "px";
        } 
    }

    ngOnInit() {

    }
}
