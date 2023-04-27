import { TestBed } from '@angular/core/testing';

import { JwtactiveguardService } from './jwtactiveguard.service';

describe('JwtactiveguardService', () => {
  let service: JwtactiveguardService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(JwtactiveguardService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
