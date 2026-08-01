import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Topbar } from '../layouts/topbar/topbar';

interface CalendarDay {
  day: number;
  dim: boolean;
  event?: { time: string; variant: 'solid' | 'light' };
}

interface Consultation {
  name: string;
  specialty: string;
  date: string;
  avatar: string;
}

@Component({
  selector: 'app-appointments',
  standalone: true,
  imports: [CommonModule, Topbar],
  templateUrl: './appointments.html',
  styleUrl: './appointments.css'
})
export class Appointments {
  weekdays = ['SUN', 'MON', 'TUE', 'WED', 'THU', 'FRI', 'SAT'];

  calendarDays: CalendarDay[] = [
    // week 1 (Sept tail)
    { day: 24, dim: true }, { day: 25, dim: true }, { day: 26, dim: true },
    { day: 27, dim: true }, { day: 28, dim: true }, { day: 29, dim: true }, { day: 30, dim: true },
    // week 2
    { day: 1, dim: false }, { day: 2, dim: false }, { day: 3, dim: false },
    { day: 4, dim: false, event: { time: '8:00...', variant: 'light' } },
    { day: 5, dim: false }, { day: 6, dim: false }, { day: 7, dim: false },
    // week 3
    { day: 8, dim: false }, { day: 9, dim: false }, { day: 10, dim: false }, { day: 11, dim: false },
    { day: 12, dim: false, event: { time: '9:30...', variant: 'solid' } },
    { day: 13, dim: false, event: { time: '3:30 PM', variant: 'light' } },
    { day: 14, dim: false },
    // week 4
    { day: 15, dim: false }, { day: 16, dim: false }, { day: 17, dim: false },
    { day: 18, dim: false }, { day: 19, dim: false }, { day: 20, dim: false }, { day: 21, dim: false },
    // week 5
    { day: 22, dim: false }, { day: 23, dim: false }, { day: 24, dim: false },
    { day: 25, dim: false }, { day: 26, dim: false }, { day: 27, dim: false }, { day: 28, dim: false },
    // week 6 (Nov head)
    { day: 29, dim: false }, { day: 30, dim: false }, { day: 31, dim: false },
    { day: 1, dim: true }, { day: 2, dim: true }, { day: 3, dim: true }, { day: 4, dim: true }
  ];

  nextAppointment = {
    doctor: 'Dr. Sarah Mitchell',
    specialty: 'Cardiologist • St. Luke Hospital',
    date: 'Oct 14, 2023 at 09:30 AM',
    location: 'Telehealth (Online Call)',
    status: 'Confirmed',
    avatar: 'https://i.pravatar.cc/80?img=47'
  };

  consultations: Consultation[] = [
    { name: 'Dr. James Wilson', specialty: 'Neurology', date: 'Aug 29', avatar: 'https://i.pravatar.cc/60?img=51' },
    { name: 'Dr. Emily Chen', specialty: 'Pediatrics', date: 'Sep 18', avatar: 'https://i.pravatar.cc/60?img=32' },
    { name: 'Dr. Robert Lang', specialty: 'Orthopedics', date: 'Aug 30', avatar: 'https://i.pravatar.cc/60?img=14' }
  ];
}
