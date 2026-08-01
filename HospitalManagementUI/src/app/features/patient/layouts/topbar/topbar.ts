import { Component,Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-topbar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './topbar.html',
  styleUrl: './topbar.css',
})
export class Topbar {
  @Input() searchPlaceholder = 'Search...';
  @Input() showSearch = true;
  @Input() avatarUrl = 'https://i.pravatar.cc/80?img=12';
}
