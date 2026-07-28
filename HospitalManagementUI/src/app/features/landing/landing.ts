import { Component, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

interface WhyCard {
  icon: string;
  title: string;
  text: string;
}

interface FeatureCard {
  icon: string;
  title: string;
  text: string;
  variant: 'light' | 'filled' | 'tint';
}

interface Step {
  number: number;
  title: string;
  text: string;
}

interface Testimonial {
  initials: string;
  name: string;
  role: string;
  quote: string;
}

interface FaqItem {
  question: string;
  answer: string;
  open: boolean;
}

@Component({
  selector: 'app-landing',
  standalone: true,
  imports: [RouterLink,CommonModule],
  templateUrl: './landing.html',
  styleUrl: './landing.css'
})

export class LandingPage {
  mobileNavOpen = signal(false);

  whyCards: WhyCard[] = [
    {
      icon: 'shield',
      title: 'Enterprise Security',
      text: "HIPAA-compliant infrastructure ensuring your patients' sensitive data remains encrypted and safe at every touchpoint."
    },
    {
      icon: 'bolt',
      title: 'Operational Efficiency',
      text: 'Automated scheduling and billing modules reduce administrative overhead by up to 40%, letting staff focus on patient care.'
    },
    {
      icon: 'users',
      title: 'Patient-Centric',
      text: 'Empower patients with a seamless portal for medical records, telemedicine, and simplified appointment booking.'
    }
  ];

  featureCards: FeatureCard[] = [
    {
      icon: 'calendar',
      title: 'Smart Appointments',
      text: 'AI-driven scheduling to minimize wait times and maximize provider availability across departments.',
      variant: 'light'
    },
    {
      icon: 'folder',
      title: 'Electronic Health Records',
      text: 'Unified medical history accessible securely from any device by authorized personnel.',
      variant: 'filled'
    },
    {
      icon: 'people',
      title: 'Patient 360',
      text: 'Complete demographic, financial, and clinical profiles for improved personalized care.',
      variant: 'light'
    },
    {
      icon: 'chart',
      title: 'Doctor Dashboard',
      text: 'Real-time metrics on patient outcomes, surgeries, and daily consultation loads.',
      variant: 'light'
    },
    {
      icon: 'lock',
      title: 'Role-Based Access',
      text: 'Granular permissions ensuring the right people see the right data at the right time.',
      variant: 'tint'
    }
  ];

  steps: Step[] = [
    { number: 1, title: 'Register', text: 'Quick onboarding for patients and clinical staff with digital consent forms.' },
    { number: 2, title: 'Book Appointment', text: 'Intuitive interface for patients to select departments, doctors, and time slots.' },
    { number: 3, title: 'Receive Care', text: 'Connected diagnosis, electronic prescriptions, and digital follow-up plans.' }
  ];

  stats = [
    { value: '500+', label: 'HOSPITALS PARTNERED' },
    { value: '1M+', label: 'PATIENTS MANAGED' },
    { value: '10k+', label: 'REGISTERED DOCTORS' }
  ];

  testimonials: Testimonial[] = [
    {
      initials: 'SJ',
      name: 'Dr. Sarah Jenkins',
      role: 'Medical Director, City General',
      quote: "Transitioning to CarePulse was the best operational decision we've made in a decade. Our staff burnout rates have decreased significantly."
    },
    {
      initials: 'JM',
      name: 'James T. Morrison',
      role: 'Patient Portal User',
      quote: 'The patient app is incredible. I can see my test results immediately and message my doctor without any hassle. Truly seamless care.'
    },
    {
      initials: 'ER',
      name: 'Nurse Elena Rodriguez',
      role: 'Lead RN, Sunrise Clinic',
      quote: 'Recording vitals and checking patient history has never been faster. It allows me to spend more time actually talking to my patients.'
    }
  ];

  faqs = signal<FaqItem[]>([
    {
      question: 'Is CarePulse HIPAA compliant?',
      answer: 'Yes, CarePulse is fully HIPAA and GDPR compliant. We use enterprise-grade AES-256 encryption for all data at rest and in transit.',
      open: true
    },
    {
      question: 'How long does implementation take?',
      answer: 'Typically, a standard installation for a mid-sized clinic takes 2-4 weeks, including staff training and data migration.',
      open: true
    },
    {
      question: 'Do you support legacy data migration?',
      answer: 'Absolutely. Our specialist data team handles migrations from all major legacy systems with 99.9% data integrity assurance.',
      open: true
    }
  ]);

  toggleFaq(index: number): void {
    this.faqs.update((items) =>
      items.map((item, i) => (i === index ? { ...item, open: !item.open } : item))
    );
  }

  toggleMobileNav(): void {
    this.mobileNavOpen.update((v) => !v);
  }
}
