import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { User } from '../../../core/models/user.model';

@Component({
  selector: 'app-custom-multi-select',
  templateUrl: './custom-multi-select.component.html',
  styleUrls: ['./custom-multi-select.component.css']
})
export class CustomMultiSelectComponent implements OnInit {

  @Input() options: User[] = [];
  @Input() selectedItems: string[] = [];
  @Output() selectionChange = new EventEmitter<string[]>();

  isOpen = false;

 
  constructor() { }
  dropdownList: { id: string; name: string; }[] = [];


  ngOnInit() {
    setTimeout(()=>{
      console.log(this.options)
    },3000)
  
    this.dropdownList = this.options.map(option => ({ id: option.id, name: option.name }));
    console.log("drop", this.dropdownList)
  }

  toggleDropdown() {
    this.isOpen = !this.isOpen;
    console.log(this.isOpen)
  }

  isSelected(id: string): boolean {
    return this.selectedItems.includes(id);
  }

  onSelectItem(id: string) {
    if (this.isSelected(id)) {
      this.selectedItems = this.selectedItems.filter(item => item !== id);
    } else {
      this.selectedItems.push(id);
    }
    this.selectionChange.emit(this.selectedItems);
  }

  onSelectAll() {
    this.selectedItems = this.options.map(option => option.id);
    this.selectionChange.emit(this.selectedItems);
  }

  onDeselectAll() {
    this.selectedItems = [];
    this.selectionChange.emit(this.selectedItems);
  }

}
