import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-tech-filter',
  imports: [CommonModule],
  templateUrl: './tech-filter.component.html',
  styleUrl: './tech-filter.component.css',
})
export class TechFilterComponent {
  @Input() techList: { name: string; image: string }[] = [];

  @Output() filterCourses = new EventEmitter<string>();
  // pass name of technology is clicked
  selectTechnology(techName: string) {
    this.filterCourses.emit(techName);
  }
}
