import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';

type EntryType = 'diagnosis' | 'treatment' | 'lab';

interface TimelineEntry {
  type: EntryType;
  tag: string;
  title: string;
  date: string;
  description: string;
  physician?: string;
  pills?: string[];
  stats?: { label: string; value: string; danger?: boolean }[];
}

interface MonthGroup {
  month: string;
  entries: TimelineEntry[];
}

interface RecentDocument {
  name: string;
  meta: string;
}

@Component({
  selector: 'app-medical-history',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './history.html',
  styleUrl: './history.css'
})
export class MedicalHistory {
  view = signal<'timeline' | 'documents'>('timeline');

  months: MonthGroup[] = [
    {
      month: 'SEPTEMBER 2024',
      entries: [
        {
          type: 'diagnosis',
          tag: 'DIAGNOSIS',
          title: 'Acute Bronchitis',
          date: 'Sep 14, 10:30 AM',
          description: 'Secondary to viral infection. Prescription issued for symptom management.',
          physician: 'Dr. Michael Chen • Pulmonology'
        },
        {
          type: 'treatment',
          tag: 'TREATMENT',
          title: 'Annual Influenza Vaccine',
          date: 'Sep 05, 09:15 AM',
          description: 'Quadrivalent vaccine administered. No immediate adverse reactions noted.',
          pills: ['Lot #FL-22941', 'Main Clinic']
        }
      ]
    },
    {
      month: 'AUGUST 2024',
      entries: [
        {
          type: 'lab',
          tag: 'LAB REPORT',
          title: 'Full Blood Count (FBC)',
          date: 'Aug 22, 02:45 PM',
          description: 'Routine screening. All parameters within normal ranges except slight Vitamin D deficiency.',
          stats: [
            { label: 'HEMOGLOBIN', value: '14.2 g/dL' },
            { label: 'WBC COUNT', value: '7.5 x10⁹/L' },
            { label: 'PLATELETS', value: '210 x10⁹/L' },
            { label: 'VITAMIN D', value: '18 ng/mL', danger: true }
          ]
        }
      ]
    }
  ];

  healthSnapshot = {
    totalVisits: 12,
    pendingLabResults: 1,
    treatmentProgress: 65
  };

  recentDocuments: RecentDocument[] = [
    { name: 'Referral_Cardiology.pdf', meta: 'Oct 12, 2024 • 1.2 MB' },
    { name: 'Medical_Certificate_09...', meta: 'Sep 15, 2024 • 450 KB' },
    { name: 'MRI_Scan_Lumbar.zip', meta: 'Aug 02, 2024 • 42 MB' }
  ];

  archiveCount = 24;

  setView(view: 'timeline' | 'documents'): void {
    this.view.set(view);
  }
}
