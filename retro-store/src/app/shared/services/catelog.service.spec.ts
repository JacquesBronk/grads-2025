import { TestBed } from '@angular/core/testing';

import { CatelogService } from './catelog.service';

describe('CatelogService', () => {
  let service: CatelogService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CatelogService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
