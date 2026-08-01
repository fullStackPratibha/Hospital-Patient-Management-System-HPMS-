import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaytientLayout } from './patient-layout';

describe('PaytientLayout', () => {
  let component: PaytientLayout;
  let fixture: ComponentFixture<PaytientLayout>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PaytientLayout],
    }).compileComponents();

    fixture = TestBed.createComponent(PaytientLayout);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
