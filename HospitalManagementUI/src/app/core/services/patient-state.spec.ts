import { TestBed } from '@angular/core/testing';

import { PatientState } from './patient-state.js';

describe('PatientState', () => {
  let service: PatientState;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PatientState);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
