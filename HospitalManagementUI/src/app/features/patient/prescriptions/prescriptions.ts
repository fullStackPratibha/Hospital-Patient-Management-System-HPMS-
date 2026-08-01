import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';

interface Medication {
  name: string;
  badge: string;
  badgeVariant: 'progress' | 'daily' | 'supplement';
  doctor: string;
  department: string;
  stats: { label: string; value: string }[];
  footerLabel: string;
  footerValue: string;
  progress?: number;
}

@Component({
  selector: 'app-prescriptions',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './prescriptions.html',
  styleUrl: './prescriptions.css'
})
export class Prescriptions {
  activeTab = signal<'active' | 'history'>('active');

  medications: Medication[] = [
    {
      name: 'Amoxicillin 500mg',
      badge: 'IN PROGRESS',
      badgeVariant: 'progress',
      doctor: 'Dr. Robert Chen',
      department: 'Internal Medicine',
      stats: [
        { label: 'DOSAGE', value: '1 Tablet' },
        { label: 'FREQUENCY', value: '3x Daily' },
        { label: 'TIMING', value: 'After Meals' }
      ],
      footerLabel: 'Remaining Duration',
      footerValue: '5 days left'
    },
    {
      name: 'Lisinopril 10mg',
      badge: 'DAILY CARE',
      badgeVariant: 'daily',
      doctor: 'Dr. Sarah Jenkins',
      department: 'Cardiology',
      stats: [
        { label: 'DOSAGE', value: '1 Capsule' },
        { label: 'FREQUENCY', value: '1x Daily' },
        { label: 'TIMING', value: 'Morning' }
      ],
      footerLabel: 'Refills Available',
      footerValue: '1 of 3 left',
      progress: 33
    },
    {
      name: 'Vitamin D3 2000IU',
      badge: 'SUPPLEMENT',
      badgeVariant: 'supplement',
      doctor: 'Dr. Robert Chen',
      department: 'Internal Medicine',
      stats: [
        { label: 'DOSAGE', value: '1 Drop' },
        { label: 'FREQUENCY', value: 'Every Morning' },
        { label: 'DURATION', value: 'Indefinite' }
      ],
      footerLabel: 'Next Checkup',
      footerValue: 'Oct 24, 2023'
    }
  ];

  selectTab(tab: 'active' | 'history'): void {
    this.activeTab.set(tab);
  }
}
