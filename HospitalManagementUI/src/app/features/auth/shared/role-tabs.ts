import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-role-tabs',
  standalone: true,
  imports: [RouterLink],
  template: `
    <div class="role-tabs">
      <a routerLink="/patient-register" class="role-tab" [class.active]="active === 'patient'">PATIENT</a>
      <a routerLink="/doctor-register" class="role-tab" [class.active]="active === 'doctor'">DOCTOR</a>
    </div>
  `,
  styles: [`
    .role-tabs {
      display: flex;
      gap: 12px;
    }

    .role-tab {
      flex: 1;
      text-align: center;
      padding: 15px 0;
      border-radius: 12px;
      background: #eef0f5;
      color: #5c6478;
      font-size: 13px;
      font-weight: 700;
      letter-spacing: 0.06em;
      transition: background 0.15s ease, color 0.15s ease, box-shadow 0.15s ease;
    }

    .role-tab.active {
      background: #123ea8;
      color: #ffffff;
      box-shadow: 0 8px 18px rgba(18, 62, 168, 0.3);
    }
  `]
})
export class RoleTabsComponent {
  @Input() active: 'patient' | 'doctor' = 'patient';
}
