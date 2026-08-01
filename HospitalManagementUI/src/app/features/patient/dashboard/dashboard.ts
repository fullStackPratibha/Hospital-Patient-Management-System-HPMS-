import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Topbar } from '../layouts/topbar/topbar';


interface StatCard {
  icon: 'heart' | 'pressure' | 'sleep';
  label: string;
  value: string;
  unit: string;
  trend: string;
  trendDirection: 'up' | 'down';
  trendColor: 'green' | 'blue';
  status?: string;
}

interface MedicalRecord {
  icon: 'file' | 'scan';
  title: string;
  subtitle: string;
}

interface Prescription {
  name: string;
  instructions: string;
  footerLabel: string;
  footerVariant: 'default' | 'warning';
  actionLabel: string;
}

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, Topbar],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard {
  patientName = 'Alexander';

  stats: StatCard[] = [
    { icon: 'heart', label: 'Heart Rate', value: '72', unit: 'bpm', trend: '2%', trendDirection: 'up', trendColor: 'green' },
    { icon: 'pressure', label: 'Blood Pressure', value: '120/80', unit: 'mmHg', trend: '', trendDirection: 'up', trendColor: 'blue', status: 'Stable' },
    { icon: 'sleep', label: 'Sleep', value: '7h 20m', unit: 'Quality', trend: '12%', trendDirection: 'up', trendColor: 'blue' }
  ];

  records: MedicalRecord[] = [
    { icon: 'file', title: 'Blood Test Results', subtitle: 'Comprehensive Metabolic Panel • 2 days ago' },
    { icon: 'scan', title: 'Chest X-Ray', subtitle: 'Routine Checkup • 1 month ago' }
  ];

  prescriptions: Prescription[] = [
    {
      name: 'Lisinopril 10mg',
      instructions: 'Take 1 tablet daily in the morning',
      footerLabel: '12 refills left',
      footerVariant: 'default',
      actionLabel: 'Order Refill'
    },
    {
      name: 'Metformin 500mg',
      instructions: 'Take 2 tablets with dinner',
      footerLabel: 'Low on stock',
      footerVariant: 'warning',
      actionLabel: 'Refill Now'
    }
  ];
}
